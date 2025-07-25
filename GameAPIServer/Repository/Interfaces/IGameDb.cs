﻿using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using GameAPIServer.Models.GameDB;
using GameAPIServer.DTO.ServiceDTO;
using GameAPIServer.DTO.ControllerDTO;

namespace GameAPIServer.Repository.Interfaces;

public interface IGameDb
{
    // User related methods
    public Task<(ErrorCode, Int64)> CreateUser(AccountInfo userInfo);
    public Task<(ErrorCode, Int64)> FindUserByPlatformId(Int64 platformId);


    // Mail related methods
    public Task<(List<Mail>, DateTime?)> GetMailListAsync(Int64 accountId, DateTime? cursor, int limit);
    public Task<ErrorCode> DeleteMailAsync(Int64 mailId);
    public Task<(ErrorCode, List<MailRewardDto>)>  ClaimMailRewardAsync(Int64 mailId);
    public Task<ErrorCode> UpdateMailClaimStatusAsync(Int64 mailId);
    public Task<ErrorCode> UpdateUserRewardsAsync(Int64 accountId, List<(int? RewardId, string RewardType, int? RewardQty)> rewards);

    //public IDbConnection GDbConnection();
}
