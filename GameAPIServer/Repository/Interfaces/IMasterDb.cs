using System;
using System.Threading.Tasks;

using GameAPIServer.Models.MasterDB;
using GameAPIServer.Models;

namespace GameAPIServer.Repository.Interfaces;

public interface IMasterDb
{
    public GameAPIServer.Models.MasterDB.Version _version { get; }

    public Task<bool> Load();


    // 출석부 관련 메소드
    public Task<AttendanceBook> GetAttendanceBookAsync(Int64 attendance_book_id);
    public Task<AttendanceBook> GetAttendanceBookConsideringValidityAsync(Int64 attendance_book_id, DateTime utcNow);
    public Task<RewardData> GetRewardInfoInAttendanceBookAsync(Int64 attendanceBookId, int DayInAttendanceBook, DateTime utcNow);

}
