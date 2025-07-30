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
    public async Task<(ErrorCode, RewardData)> CheckTodayAsync(Int64 accountUid, Int64 attendanceBookId, DateTime utcNow)
    {
        // Call the repository method to get attendance data
        var attendance = new Attendance();
        var rewardData = new RewardData();

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
            var attendanceBook = await _masterDb.GetAttendanceBookAsync(attendanceBookId);
            if (attendanceBook == null || attendanceBook.end_dt < utcNow)
            {
                return (ErrorCode.MasterDbGetAttendanceBookError, null);
            }
        }
        catch (Exception e)
        {
            return (ErrorCode.MasterDbGetAttendanceBookError, null);
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

