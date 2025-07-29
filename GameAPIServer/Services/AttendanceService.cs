using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameAPIServer.Models.GameDB;
using GameAPIServer.Models.MasterDB;
using GameAPIServer.Repository.Interfaces;
using GameAPIServer.Services.Interfaces;

namespace GameAPIServer.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IMasterDb _masterDb;

        public AttendanceService(IAttendanceRepository attendanceRepository, IMasterDb masterDb)
        {
            _masterDb = masterDb;
            _attendanceRepository = attendanceRepository;
        }

        public async Task<(ErrorCode,RewardData)> CheckInTodayAsync(Int64 accountUid, Int64 attendanceBookId)
        {
            // Call the repository method to get attendance data

            var attendance = new Attendance();
            var rewardData = new RewardData();

            // 내 출석부 정보 가져오기
            try
            {
                attendance = await _attendanceRepository.GetAttendanceAsync(accountUid, attendanceBookId);
            }
            catch (Exception ex)
            {
                return (ErrorCode.DatabaseError, null);
            }

            try
            {
                rewardData = await _masterDb.GetRewardInAttendanceBookAsync(attendanceBookId, attendance.attendance_continue_cnt);
            }
            catch (Exception ex)
            {
                return (ErrorCode.DatabaseError, null);
            }

            // 출석부에 오늘 날짜로 출석 체크
            try
            {
                await _attendanceRepository.CheckInAttendanceAsync(accountUid, attendanceBookId);
            }
            catch (Exception ex)
            {
                return (ErrorCode.DatabaseError, null);
            }


            return rewardData;
        }

        // public async Task<int> UpdateAttendanceAsync(Attendance attendance)
        // {
        //     return await _attendanceRepository.UpdateAttendanceAsync(attendance);
        // }

        // public async Task<int> InsertAttendanceAsync(Attendance attendance)
        // {
        //     return await _attendanceRepository.InsertAttendanceAsync(attendance);
        // }

        // public async Task<List<Attendance>> GetAttendanceListAsync(long accountUid)
        // {
        //     return await _attendanceRepository.GetAttendanceListAsync(accountUid);
        // }
    }
}
