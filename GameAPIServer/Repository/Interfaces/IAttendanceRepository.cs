using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using GameAPIServer.Models.GameDB;

namespace GameAPIServer.Repository.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<Attendance> GetAttendanceAsync(Int64 accountUid, Int64 attendanceBookId);
        Task<int> UpdateAttendanceAsync(Attendance attendance);
        Task<int> InsertAttendanceAsync(Attendance attendance);
        Task<bool> CheckInAttendanceAsync(Int64 accountUid, Int64 attendanceBookId);
        // Task<List<Attendance>> GetAttendanceListAsync(Int64 accountUid);
    }
}
