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
        var key = MemoryDbKeyMaker.TokenKey(token);
        var result = ErrorCode.None;

        var user = new RdbAuthUserData
        {
            AccountUid = account_uid,
            TokenKey = token
        };

        try
        {
            var redis = new RedisString<RdbAuthUserData>(_redisConn, token, LoginTimeSpan());
            if (!await redis.SetAsync(user, LoginTimeSpan()))
            {
                _logger.ZLogError($"[SetTokenRedisAsync] Uid:{account_uid}, Token:{token}, ErrorMessage:UserBasicAuth, RedisString set Error");
                result = ErrorCode.LoginFailAddRedis;
                return result;
            }
        }
        catch
        {
            _logger.ZLogError($"[SetTokenRedisAsync] Uid:{account_uid}, Token:{token}, ErrorMessage:Redis Connection Error");
            result = ErrorCode.LoginFailAddRedis;
            return result;
        }

        return result;
    }


    public async Task<bool> SetUserReqLockAsync(string key)
    {
        try
        {
            RedisString<RdbAuthUserData> redis = new(_redisConn, key, NxKeyTimeSpan());
            if (await redis.SetAsync(new RdbAuthUserData
            {
                // emtpy value
            }, NxKeyTimeSpan(), StackExchange.Redis.When.NotExists) == false)
            {
                return false;
            }
        }
        catch
        {
            _logger.ZLogError($"[SetUserReqLockAsync] Key = {key}, ErrorMessage:Redis Connection Error");
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

        try
        {
            RedisString<RdbAuthUserData> redis = new(_redisConn, key, null);
            var redisResult = await redis.DeleteAsync();
            return redisResult;
        }
        catch
        {
            _logger.ZLogError($"[DelUserReqLockAsync] Key = {key}, ErrorMessage:Redis Connection Error");
            return false;
        }
    }


    public async Task<(bool, RdbAuthUserData)> GetUserAsync(string token)
    {
        var tokenKey = MemoryDbKeyMaker.TokenKey(token);

        try
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
        catch
        {
            _logger.ZLogError($"[GetUserAsync] Token:{token},ErrorMessage:ID does Not Exist");
            return (false, null);
        }
    }


    public async Task<ErrorCode> DelUserAuthAsync(string token)
    {
        try
        {
            var redis = new RedisString<RdbAuthUserData>(_redisConn, token, null);
            await redis.DeleteAsync();
            return ErrorCode.None;
        }
        catch
        {
            _logger.ZLogError($"[DelUserAuthAsync] Token:{token}, ErrorMessage:Redis Connection Error");
            return ErrorCode.LogoutRedisDelFailException;
        }
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