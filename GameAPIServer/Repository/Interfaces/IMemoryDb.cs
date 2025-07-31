using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using GameAPIServer.Models;

namespace GameAPIServer.Repository.Interfaces;

public interface IMemoryDb
{
    public Task<ErrorCode> SetTokenAsync(string token, Int64 player_uid);
    public Task<ErrorCode> DelUserAuthAsync(string token);

    public Task<(bool, RdbAuthUserData)> GetUserAsync(string token);
    public Task<(ErrorCode,Int64)> GetUserIdAsync(string token);

    public Task<bool> SetUserReqLockAsync(string token);
    public Task<bool> DelUserReqLockAsync(string token);
}
