using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SqlKata.Execution;
using GameAPIServer.Models.GameDB;
using GameAPIServer.Repository.Interfaces;

namespace GameAPIServer.Repository
{
    public partial class GameDb : IAttendanceRepository
    {
        public async Task<Attendance> GetAttendanceAsync(Int64 accountUid, Int64 attendanceBookId)
        {
            return await _queryFactory.Query("attendance")
                .Where("account_uid", accountUid)
                .Where("attendance_book_id", attendanceBookId)
                .FirstOrDefaultAsync<Attendance>();
        }

        public async Task<int> UpdateAttendanceAsync(Attendance attendance)
        {
            return await _queryFactory.Query("attendance")
                .Where("account_uid", attendance.account_uid)
                .Where("attendance_book_id", attendance.attendance_book_id)
                .UpdateAsync(new
                {
                    last_attendance_dt = attendance.last_attendance_dt,
                    attendance_continue_cnt = attendance.attendance_continue_cnt
                });
        }

        public async Task<int> InsertAttendanceAsync(Attendance attendance)
        {
            return await _queryFactory.Query("attendance").InsertAsync(attendance);
        }

        public async Task<bool> CheckInAttendanceAsync(Int64 accountUid, Int64 attendanceBookId)
        {
            var query = _queryFactory.Query("attendance")
                .Where("account_uid", accountUid)
                .Where("attendance_book_id", attendanceBookId);
            var result = (await query.IncrementAsync("attendance_continue_cnt", 1)) > 0;

            return result;
        }

        // public async Task<List<Attendance>> GetAttendanceListAsync(Int64 accountUid)
        // {
        //     return await _queryFactory.Query("attendance")
        //         .Where("account_uid", accountUid)
        //         .GetAsync<Attendance>();
        // }
    }
}
