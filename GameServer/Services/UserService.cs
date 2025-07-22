using System;
using Microsoft.Extensions.Logging;
using GameAPIServer.Repository.Interfaces;
using GameAPIServer.Services.Interfaces;

namespace GameAPIServer.Services;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IGameDb _gameDb;
    private readonly IMemoryDb _memoryDb;

    public UserService(
        ILogger<UserService> logger,
        IGameDb gameDb,
        IMemoryDb memoryDb)
    {
        _logger = logger;
        _gameDb = gameDb;
        _memoryDb = memoryDb;
    }

    // public async Task<(ErrorCode, GdbUserInfo)> GetUserInfo(int uid)
    // {
    //     try
    //     {
    //         return (ErrorCode.None, await _gameDb.GetUserByUid(uid));
    //     }
    //     catch (Exception e)
    //     {
    //         _logger.ZLogError(e,
    //             $"[User.GetUserInfo] ErrorCode: {ErrorCode.UserInfoFailException}, Uid: {uid}");
    //         return (ErrorCode.UserInfoFailException, null);
    //     }
    // }
}
