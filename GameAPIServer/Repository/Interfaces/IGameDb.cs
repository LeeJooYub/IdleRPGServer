using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using GameAPIServer.Models.GameDB;

namespace GameAPIServer.Repository.Interfaces;

public interface IGameDb
{
    // User related methods
    public Task<(ErrorCode, Int64)> CreateUser(AccountInfo userInfo);
    public Task<(ErrorCode, Int64)> FindUserByPlatformId(Int64 platformId);


    // Mail related methods
    public Task<List<Mail>> GetMailListAsync(Int64 accountId, DateTime? cursor, int limit);
    public Task<ErrorCode> UpdateUserRewardsAsync(Int64 accountId, RewardInMail reward);
    public Task<(ErrorCode, RewardInMail)> ClaimMailRewardAsync(Int64 mailId);
    public Task<List<RewardInMail>> GetRewardsToClaim(Int64 accountId);
    public Task MarkAllMailsClaimed(Int64 accountId);
    public Task<ErrorCode> UpdateUserRewardsAllAsync(Int64 accountId, List<RewardInMail> rewards);

    //public IDbConnection GDbConnection();
}
