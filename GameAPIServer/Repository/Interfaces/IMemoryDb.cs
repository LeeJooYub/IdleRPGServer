﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using GameAPIServer.Models;

namespace GameAPIServer.Repository.Interfaces;

public interface IMemoryDb
{
    public Task<ErrorCode> ActivateGameTokenAsync(string game_server_token, Int64 account_id);

    // public Task<ErrorCode> CheckUserAuthAsync(string id, string authToken);
    public Task<(bool, RdbAuthUserData)> GetUserAsync(string id);
    public Task<bool> SetUserReqLockAsync(string key);
    // public Task<bool> LockUserReqAsync(string key);
    // public Task<bool> UnLockUserReqAsync(string key);
    public Task<ErrorCode> DelUserAuthAsync(Int64 account_id);
    public Task<bool> DelUserReqLockAsync(string key);
    // public  Task<ErrorCode> SetUserScore(Int64 uid, int score);
    // public  Task<(ErrorCode, List<RankData>)> GetTopRanking();
    // public Task<(ErrorCode, Int64)> GetUserRankAsync(Int64 uid);
}
