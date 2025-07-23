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

    public async Task<ErrorCode> ActivateGameTokenAsync(string token, Int64 account_id)
    {
        var key = MemoryDbKeyMaker.MakeUIDKey(account_id.ToString());
        var result = ErrorCode.None;

        var user = new RdbAuthUserData
        {
            AccountId = account_id,
            GameServerToken = token
        };

        try
        {
            var redis = new RedisString<RdbAuthUserData>(_redisConn, key, LoginTimeSpan());
            if (!await redis.SetAsync(user, LoginTimeSpan()))
            {
                _logger.ZLogError($"[ActivateGameTokenAsync] Uid:{account_id}, Token:{token}, ErrorMessage:UserBasicAuth, RedisString set Error");
                result = ErrorCode.LoginFailAddRedis;
                return result;
            }
        }
        catch
        {
            _logger.ZLogError($"[ActivateGameTokenAsync] Uid:{account_id}, Token:{token}, ErrorMessage:Redis Connection Error");
            result = ErrorCode.LoginFailAddRedis;
            return result;
        }

        return result;
    }

    public async Task<ErrorCode> DelUserAuthAsync(Int64 account_id)
    {
        try
        {
            var redis = new RedisString<RdbAuthUserData>(_redisConn, MemoryDbKeyMaker.MakeUIDKey(account_id.ToString()), null);
            await redis.DeleteAsync();
            return ErrorCode.None;
        }
        catch
        {
            _logger.ZLogError($"[DelUserAuthAsync] Uid:{account_id}, ErrorMessage:Redis Connection Error");
            return ErrorCode.LogoutRedisDelFailException;
        }
    }

    public TimeSpan LoginTimeSpan()
    {
        return TimeSpan.FromMinutes(RediskeyExpireTime.LoginKeyExpireMin);
    }
}