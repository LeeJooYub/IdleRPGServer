﻿using GameAPIServer.Models.DAO;
using GameAPIServer.Models.DTO;
using GameAPIServer.Repository.Interfaces;
using GameAPIServer.Servicies.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using ZLogger;

namespace GameAPIServer.Servicies;

public class UserService : IUserService
{
    readonly ILogger<UserService> _logger;
    readonly IGameDb _gameDb;
    readonly IMemoryDb _memoryDb;

    public UserService(ILogger<UserService> logger, IGameDb gameDb, IMemoryDb memoryDb)
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

    // public async Task<(ErrorCode, GdbUserMoneyInfo)> GetUserMoneyInfo(int uid)
    // {
    //     try
    //     {
    //         return (ErrorCode.None, await _gameDb.GetUserMoneyById(uid));
    //     }
    //     catch (Exception e)
    //     {
    //         _logger.ZLogError(e,
    //             $"[User.GetUserMoneyInfo] ErrorCode: {ErrorCode.UserMoneyInfoFailException}, Uid: {uid}");
    //         return (ErrorCode.UserMoneyInfoFailException, null);
    //     }
    // }

    

    // public async Task<(ErrorCode, OtherUserInfo)> GetOtherUserInfo(int uid)
    // {
    //     try
    //     {
    //         var userInfo = await _gameDb.GetUserByUid(uid);
    //         if (userInfo == null)
    //         {
    //             _logger.ZLogError($"[User.GetOtherUserInfo] ErrorCode: {ErrorCode.UserNotExist}, Uid: {uid}");
    //             return (ErrorCode.UserNotExist, null);
    //         }

            
    //         var (errorCode, rank) = await _memoryDb.GetUserRankAsync(uid);

    //         if(errorCode != ErrorCode.None)
    //         {
    //             _logger.ZLogError($"[User.GetOtherUserInfo] ErrorCode: {errorCode}, Uid: {uid}");
    //             return (errorCode, null);
    //         }

    //         return (ErrorCode.None, new OtherUserInfo
    //         {
    //             uid = uid,
    //             nickname = userInfo.nickname,                    
    //             rank = rank,
    //         });

    //     }
    //     catch (Exception e)
    //     {
    //         _logger.ZLogError(e,
    //             $"[User.GetOtherUserInfo] ErrorCode: {ErrorCode.GetOtherUserInfoFailException}, Uid: {uid}");
    //         return (ErrorCode.GetOtherUserInfoFailException, null);
    //     }
    // }
}
