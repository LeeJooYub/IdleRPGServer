using System;
using System.Threading.Tasks;

using CloudStructures;
using CloudStructures.Structures;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using ZLogger;

using GameAPIServer.Models;
using GameAPIServer.Repository.Interfaces;

namespace GameAPIServer.Repository;

public class MemoryDb : IMemoryDb
{
    private readonly RedisConnection _redisConn;
    private readonly ILogger<MemoryDb> _logger;
    private readonly IOptions<DbConfig> _dbConfig;

    public MemoryDb(ILogger<MemoryDb> logger, IOptions<DbConfig> dbConfig)
    {
        _logger = logger;
        _dbConfig = dbConfig;
        var config = new RedisConfig("default", _dbConfig.Value.Redis);
        _redisConn = new RedisConnection(config);
    }

    public async Task<ErrorCode> SetTokenAsync(string token, Int64 account_uid)
    {
        var result = ErrorCode.None;

        var user = new RdbAuthUserData
        {
            AccountUid = account_uid,
            TokenKey = token
        };

        var redis = new RedisString<RdbAuthUserData>(_redisConn, token, LoginTimeSpan());
        if (!await redis.SetAsync(user, LoginTimeSpan()))
        {
            _logger.ZLogError($"[SetTokenRedisAsync] Uid:{account_uid}, Token:{token}, ErrorMessage:UserBasicAuth, RedisString set Error");
            result = ErrorCode.LoginFailAddRedis;
            return result;
        }

        return result;
    }


    public async Task<bool> SetUserReqLockAsync(string key)
    {

        RedisString<RdbAuthUserData> redis = new(_redisConn, key, NxKeyTimeSpan());
        if (await redis.SetAsync(new RdbAuthUserData
        {
            // emtpy value
        }, NxKeyTimeSpan(), StackExchange.Redis.When.NotExists) == false)
        {
            return false;
        }


        return true;
    }
    
    public async Task<bool> DelUserReqLockAsync(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            return false;
        }

        RedisString<RdbAuthUserData> redis = new(_redisConn, key, null);
        var redisResult = await redis.DeleteAsync();
        return redisResult;
    }


    public async Task<(bool, RdbAuthUserData)> GetUserAsync(string token)
    {
        RedisString<RdbAuthUserData> redis = new(_redisConn, token, null);
        RedisResult<RdbAuthUserData> user = await redis.GetAsync();
        if (!user.HasValue)
        {
            _logger.ZLogError(
                $"[GetUserAsync] Token = {token}, ErrorMessage = Not Assigned User, RedisString get Error");
            return (false, null);
        }

        return (true, user.Value);
  
    }
    // 토큰으로 유저 ID를 가져오는 메서드
    public async Task<(ErrorCode, Int64)> GetUserIdAsync(string token)
    {
        RedisString<RdbAuthUserData> redis = new(_redisConn, token, null);
        RedisResult<RdbAuthUserData> user = await redis.GetAsync();
        if (!user.HasValue)
        {
            _logger.ZLogError($"[GetUserIdAsync] Token:{token}, ErrorMessage:ID does Not Exist");
            return (ErrorCode.AuthTokenKeyNotFound, 0);
        }

        return (ErrorCode.None, user.Value.AccountUid);
    }


    public async Task<ErrorCode> DelUserAuthAsync(string token)
    {
        var redis = new RedisString<RdbAuthUserData>(_redisConn, token, null);
        await redis.DeleteAsync();
        return ErrorCode.None;
    }

    public TimeSpan LoginTimeSpan()
    {
        return TimeSpan.FromMinutes(RediskeyExpireTime.LoginKeyExpireMin);
    }
    
    
    public TimeSpan NxKeyTimeSpan()
    {
        return TimeSpan.FromSeconds(RediskeyExpireTime.NxKeyExpireSecond);
    }
}