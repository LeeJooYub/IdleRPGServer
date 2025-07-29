using System.Threading.Tasks;
using System.Collections.Generic;
using GameAPIServer.Models.GameDB;

namespace GameAPIServer.Services.Interfaces
{
    public interface IAttendanceService
    {
        Task<Attendance> CheckInTodayAsync(long accountUid, long attendanceBookId);
        // Task<int> UpdateAttendanceAsync(Attendance attendance);
        // Task<int> InsertAttendanceAsync(Attendance attendance);
        // Task<List<Attendance>> GetAttendanceListAsync(long accountUid);
    }
}
