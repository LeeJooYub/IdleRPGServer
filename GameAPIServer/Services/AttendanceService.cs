using System;
using System.Threading.Tasks;
using GameAPIServer.Models.GameDB;
using GameAPIServer.Models.MasterDB;
using GameAPIServer.Models;
using GameAPIServer.DTO.Service;
using GameAPIServer.Repository.Interfaces;
using GameAPIServer.Services.Interfaces;

namespace GameAPIServer.Services;

public class AttendanceService : IAttendanceService
{
    private readonly IGameDb _gameDb;
    private readonly IMasterDb _masterDb;

    public AttendanceService(IGameDb gameDb, IMasterDb masterDb)
    {
        _gameDb = gameDb;
        _masterDb = masterDb;
    }

    /// <summary>
    /// WHY: 출석 체크 비즈니스 로직. 만료, 중복, 미래 체크 등 다양한 예외 상황을 처리함.
    /// </summary>
    public async Task<CheckTodayOutput> CheckTodayAsync(CheckTodayInput input)
    {
        var output = new CheckTodayOutput
        {
            ErrorCode = ErrorCode.None
        };

        Attendance attendance;
        AttendanceBook attendanceBook;
        RewardData rewardData;

        // 내 특정 출석부 정보 가져오기 (출석부 ID, 현 출석 현황)
        try
        {
            attendance = await _gameDb.GetAttendanceBookAsync(input.PlayerUid, input.AttendanceBookId);
        }
        catch (Exception)
        {
            output.ErrorCode = ErrorCode.GameDbGetAttendanceBookError;
            return output;
        }

        // 출석부 만료 여부 확인
        try
        {
            attendanceBook = await _masterDb.GetAttendanceBookAsync(input.AttendanceBookId);
            if (attendanceBook is null || attendanceBook.end_dt < input.Now)
            {
                output.ErrorCode = ErrorCode.MasterDbGetAttendanceBookError;
                return output;
            }
        }
        catch (Exception)
        {
            output.ErrorCode = ErrorCode.MasterDbGetAttendanceBookError;
            return output;
        }

        // 출석 체크 예외 처리 (중복, 미래 체크, refresh 시간 미도달)
        if (input.CheckNthDay <= attendance.attendance_continue_cnt)
        {
            output.ErrorCode = ErrorCode.GameDbAlreadyCheckInAttendanceError;
            return output;
        }
        if (input.CheckNthDay > attendance.attendance_continue_cnt + 1)
        {
            output.ErrorCode = ErrorCode.GameDbTryToCheckInFutureError;
            return output;
        }
        if (input.CheckNthDay == attendance.attendance_continue_cnt + 1 && attendanceBook.refresh_time > input.Now.TimeOfDay)
        {
            output.ErrorCode = ErrorCode.GameDbRefreshTimeNotReached;
            return output;
        }

        // 마스터 데이터에서 해당 출석부, 특정 날짜에 대한 리워드 정보 가져오기
        try
        {
            rewardData = await _masterDb.GetRewardInfoInAttendanceBookAsync(input.AttendanceBookId, attendance.attendance_continue_cnt, input.Now);
            output.Reward = rewardData;
        }
        catch (Exception)
        {
            output.ErrorCode = ErrorCode.MasterDbGetRewardInfoInAttendanceBookError;
            return output;
        }

        // 출석 체크 DB 반영
        try
        {
            await _gameDb.CheckInAttendanceBookAsync(input.PlayerUid, input.AttendanceBookId);
        }
        catch (Exception)
        {
            output.ErrorCode = ErrorCode.GameDbCheckInAttendanceBookError;
            return output;
        }

        // 리워드 지급 DB 반영
        try
        {
            var errorCode = await _gameDb.UpdateUserFromRewardAsync(input.PlayerUid, rewardData);
            if (errorCode != ErrorCode.None)
            {
                output.ErrorCode = errorCode;
                return output;
            }
        }
        catch (Exception)
        {
            output.ErrorCode = ErrorCode.GameDbUpdateUserFromRewardError;
            return output;
        }

        return output;
    }
}

