using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using GameAPIServer.Models.GameDB;
using GameAPIServer.Models.MasterDB;
using GameAPIServer.Models;
using GameAPIServer.DTO.Service;

namespace GameAPIServer.Services.Interfaces
{
    public interface IAttendanceService
    {
        Task<CheckTodayOutput> CheckTodayAsync(CheckTodayInput input);
        // Task<int> UpdateAttendanceAsync(Attendance attendance);
        // Task<int> InsertAttendanceAsync(Attendance attendance);
        // Task<List<Attendance>> GetAttendanceListAsync(long accountUid);
    }
}
