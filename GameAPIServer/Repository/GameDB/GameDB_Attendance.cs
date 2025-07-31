using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SqlKata.Execution;
using GameAPIServer.Models.GameDB;
using GameAPIServer.Repository.Interfaces;

namespace GameAPIServer.Repository
{
    public partial class GameDb : IGameDb
    {
        public async Task<Attendance> GetAttendanceBookAsync(Int64 accountUid, Int64 attendanceBookId)
        {
            return await _queryFactory.Query("user_attendance")
                .Where("player_uid", accountUid)
                .Where("attendance_book_id", attendanceBookId)
                .FirstOrDefaultAsync<Attendance>();
        }

        public async Task<int> InsertAttendanceBookAsync(Attendance attendance)
        {
            return await _queryFactory.Query("user_attendance").InsertAsync(attendance);
        }

        public async Task<bool> CheckInAttendanceBookAsync(Int64 accountUid, Int64 attendanceBookId)
        {
            var query = _queryFactory.Query("user_attendance")
                .Where("player_uid", accountUid)
                .Where("attendance_book_id", attendanceBookId);
            var result = (await query.IncrementAsync("attendance_continue_cnt", 1)) > 0;


            return result;
        }

        // public async Task<List<Attendance>> GetAttendanceListAsync(Int64 accountUid)
        // {
        //     return await _queryFactory.Query("attendance")
        //         .Where("player_uid", accountUid)
        //         .GetAsync<Attendance>();
        // }
    }
}
