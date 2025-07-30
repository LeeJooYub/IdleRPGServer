using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameAPIServer.Models.GameDB;
using GameAPIServer.Models.MasterDB;
using GameAPIServer.Models;

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
    public async Task<(ErrorCode, RewardData)> CheckTodayAsync(Int64 accountUid, Int64 attendanceBookId,int checkNthDay, DateTime utcNow)
    {
        // Call the repository method to get attendance data
        var attendance = new Attendance();
        var rewardData = new RewardData();
        var attendanceBook = new AttendanceBook();

        // 내 특정 출석부 정보 가져오기 (출석부 ID, 현 출석 현황)
        try
        {
            attendance = await _gameDb.GetAttendanceBookAsync(accountUid, attendanceBookId);
        }
        catch (Exception e)
        {
            return (ErrorCode.GameDbGetAttendanceBookError, null);
        }

        // 이미 만료된 출석부면 에러
        try
        {
            attendanceBook = await _masterDb.GetAttendanceBookAsync(attendanceBookId);
            if (attendanceBook == null || attendanceBook.end_dt < utcNow)
            {
                return (ErrorCode.MasterDbGetAttendanceBookError, null);
            }
        }
        catch (Exception e)
        {
            return (ErrorCode.MasterDbGetAttendanceBookError, null);
        }


        // 이미 출석했으면 에러. 
        if (checkNthDay <= attendance.attendance_continue_cnt)
        {
            return (ErrorCode.GameDbAlreadyCheckInAttendanceError, null);
        }
        // 순번을 밟지않고 미래의 출석을 미리 받으려는 경우 에러
        else if (checkNthDay > attendance.attendance_continue_cnt + 1)
        {
            // 출석부의 n번째 날이 현재 출석 현황보다 크면 에러
            return (ErrorCode.GameDbTryToCheckInFutureError, null);
        }
        // 출석하려는 날 == 지금까지 출석한 날 (카운트) + 1 일 경우.
        else if (checkNthDay == attendance.attendance_continue_cnt + 1)
        {
            // 현재시간이 갱신 기준시 이후가 아니라면 에러 
            if (attendanceBook.refresh_time > utcNow.TimeOfDay)
            {
                return (ErrorCode.GameDbRefreshTimeNotReached, null);
            }
        }



        // 마스터 데이터에서 해당 출석부, 특정 날짜에 대한 리워드 정보 가져오기 
        try
        {
            rewardData = await _masterDb.GetRewardInfoInAttendanceBookAsync(attendanceBookId, attendance.attendance_continue_cnt, utcNow);
        }
        catch (Exception e)
        {
            return (ErrorCode.MasterDbGetRewardInfoInAttendanceBookError, null);

        }

        // 출석 체크
        try
        {
            await _gameDb.CheckInAttendanceBookAsync(accountUid, attendanceBookId);
        }
        catch (Exception ex)
        {
            return (ErrorCode.GameDbCheckInAttendanceBookError, null);
        }

        try
        {
            ErrorCode errorCode = await _gameDb.UpdateUserFromRewardAsync(accountUid, rewardData);
            if (errorCode != ErrorCode.None)
            {
                return (errorCode, null);
            }
        }
        catch (Exception ex)
        {
            return (ErrorCode.GameDbUpdateUserFromRewardError, null);
        }


        return (ErrorCode.None, rewardData);
    }
}

