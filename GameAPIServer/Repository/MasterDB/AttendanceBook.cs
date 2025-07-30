using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameAPIServer.Models;
using GameAPIServer.Repository.Interfaces;
using GameAPIServer.Models.MasterDB;

using SqlKata.Execution;

namespace GameAPIServer.Repository;

public partial class MasterDb : IMasterDb
{
    // Reward 관련 메소드
    public async Task<RewardData> GetRewardInfoInAttendanceBookAsync(Int64 attendanceBookId, int DayInAttendanceBook, DateTime utcNow)
    {
        var query = _queryFactory.Query("day_in_attendance_book")
            .Where("attendance_book_id", attendanceBookId)
            .Where("attendance_day", DayInAttendanceBook)
            .FirstAsync<DayInAttendanceBook>();
        var dayInAttendance = await query;

        var rewardData = new RewardData
        {
            reward_type_cd = dayInAttendance?.reward_type_cd,
            reward_id = dayInAttendance?.reward_id,
            reward_qty = dayInAttendance?.reward_qty
        };

        return rewardData;
    }

    public async Task<AttendanceBook> GetAttendanceBookAsync(Int64 attendance_book_id)
    {
        var query = _queryFactory.Query("attendance_book")
            .Where("attendance_book_id", attendance_book_id)
            .FirstAsync<AttendanceBook>();
        var attendanceBooks = await query;
        return attendanceBooks;
    }

    public async Task<AttendanceBook> GetAttendanceBookConsideringValidityAsync(Int64 attendance_book_id, DateTime utcNow)
    {
        var query = _queryFactory.Query("attendance_book")
            .Where("attendance_book_id", attendance_book_id)
            .Where("start_dt", "<=", utcNow) // 현재 시간보다 이전에 시작된 출석부
            .Where("end_dt", ">=", utcNow) // 현재 시간보다 이후에 종료되는 출석부
            .FirstAsync<AttendanceBook>();
        var attendanceBooks = await query;
        return attendanceBooks;
    }
}