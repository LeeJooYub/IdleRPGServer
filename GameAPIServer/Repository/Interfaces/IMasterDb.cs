using System;
using System.Threading.Tasks;

using GameAPIServer.Models.MasterDB;
using GameAPIServer.Models;

namespace GameAPIServer.Repository.Interfaces;

public interface IMasterDb
{
    public GameAPIServer.Models.MasterDB.Version _version { get; }

    public Task<bool> Load();

  
    //Reward 관련 메소드  
    //public Task<RewardData> GetRewardInAttendanceBookAsync(Int64 attendanceBookId, int DayInAttendanceBook);
}
