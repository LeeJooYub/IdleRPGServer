using System;
using System.Collections.Generic;
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

    //TODO : 에러 코드들 정리
    public async Task<CheckTodayOutput> CheckTodayAsync(CheckTodayInput input)
    {
        // Call the repository method to get attendance data
        var checkTodayOutput = new CheckTodayOutput
        {
            ErrorCode = ErrorCode.None,
            Reward = null
        };
        var attendance = new Attendance();
        var rewardData = new RewardData();
        var attendanceBook = new AttendanceBook();

        // 내 특정 출석부 정보 가져오기 (출석부 ID, 현 출석 현황)
        try
        {
            attendance = await _gameDb.GetAttendanceBookAsync(input.PlayerUid, input.AttendanceBookId);
        }
        catch (Exception e)
        {
            checkTodayOutput.ErrorCode = ErrorCode.GameDbGetAttendanceBookError;
            return checkTodayOutput;
        }

        // 이미 만료된 출석부면 에러
        try
        {
            attendanceBook = await _masterDb.GetAttendanceBookAsync(input.AttendanceBookId);
            if (attendanceBook == null || attendanceBook.end_dt < input.Now)
            {
                checkTodayOutput.ErrorCode = ErrorCode.MasterDbGetAttendanceBookError;
                return checkTodayOutput;
            }
        }
        catch (Exception e)
        {
            checkTodayOutput.ErrorCode = ErrorCode.MasterDbGetAttendanceBookError;
            return checkTodayOutput;
        }


        // 이미 출석했으면 에러. 
        if (input.CheckNthDay <= attendance.attendance_continue_cnt)
        {
            checkTodayOutput.ErrorCode = ErrorCode.GameDbAlreadyCheckInAttendanceError;
            return checkTodayOutput;
        }
        // 순번을 밟지않고 미래의 출석을 미리 받으려는 경우 에러
        else if (input.CheckNthDay > attendance.attendance_continue_cnt + 1)
        {
            // 출석부의 n번째 날이 현재 출석 현황보다 크면 에러
            checkTodayOutput.ErrorCode = ErrorCode.GameDbTryToCheckInFutureError;
            return checkTodayOutput;
        }
        // 출석하려는 날 == 지금까지 출석한 날 (카운트) + 1 일 경우.
        else if (input.CheckNthDay == attendance.attendance_continue_cnt + 1)
        {
            // 현재시간이 갱신 기준시 이후가 아니라면 에러 
            if (attendanceBook.refresh_time > input.Now.TimeOfDay)
            {
                checkTodayOutput.ErrorCode = ErrorCode.GameDbRefreshTimeNotReached;
                return checkTodayOutput;
            }
        }



        // 마스터 데이터에서 해당 출석부, 특정 날짜에 대한 리워드 정보 가져오기 
        try
        {
            rewardData = await _masterDb.GetRewardInfoInAttendanceBookAsync(input.AttendanceBookId, attendance.attendance_continue_cnt, input.Now);
            checkTodayOutput.Reward = rewardData;
        }
        catch (Exception e)
        {
            checkTodayOutput.ErrorCode = ErrorCode.MasterDbGetRewardInfoInAttendanceBookError;
            return checkTodayOutput;

        }

        // 출석 체크
        try
        {
            await _gameDb.CheckInAttendanceBookAsync(input.PlayerUid, input.AttendanceBookId);
        }
        catch (Exception ex)
        {
            checkTodayOutput.ErrorCode = ErrorCode.GameDbCheckInAttendanceBookError;
            return checkTodayOutput;
        }

        try
        {
            ErrorCode errorCode = await _gameDb.UpdateUserFromRewardAsync(input.PlayerUid, rewardData);
            if (errorCode != ErrorCode.None)
            {
                checkTodayOutput.ErrorCode = errorCode;
                return checkTodayOutput;
            }
        }
        catch (Exception ex)
        {
            checkTodayOutput.ErrorCode = ErrorCode.GameDbUpdateUserFromRewardError;
            return checkTodayOutput;
        }


        return checkTodayOutput;
    }
}

