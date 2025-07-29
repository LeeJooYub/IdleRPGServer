using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using GameAPIServer.Repository.Interfaces;
using GameAPIServer.Services.Interfaces;

using GameAPIServer.DTO.ControllerDTO;
using GameAPIServer.DTO.ServiceDTO;
using GameAPIServer.Models.GameDB;
using GameAPIServer.Models.MasterDB;
using System.Runtime.InteropServices;

namespace GameAPIServer.Services;

public class UserDataLoadService : IUserDataLoadService
{
    private readonly ILogger<UserDataLoadService> _logger;
    private readonly IGameDb _gameDb;
    private readonly IMemoryDb _memoryDb;

    public UserDataLoadService(
        ILogger<UserDataLoadService> logger,
        IGameDb gameDb,
        IMemoryDb memoryDb)
    {
        _logger = logger;
        _gameDb = gameDb;
        _memoryDb = memoryDb;
    }

    public async Task<GetUserCurrencyOutput> GetUserCurrencyAsync(GetUserCurrencyInput input)
    {
        var errorCode = ErrorCode.None;
        var currencyList = new List<UserCurrency>();

        try
        {
            (errorCode,currencyList)= await _gameDb.GetUserCurrencyAsync(input.AccountId);
            var result = new GetUserCurrencyOutput
            {
                AccountId = input.AccountId,
                CurrencyList = currencyList,
                ErrorCode = 0 // Assuming 0 means no error
            };


            return result;
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading user currency for AccountId: {AccountId}", input.AccountId);
            var result = new GetUserCurrencyOutput
            {
                AccountId = input.AccountId,
                CurrencyList = new List<UserCurrency>(), // Return an empty list on error
                ErrorCode = ErrorCode.UserMoneyInfoFailException // Assuming -1 means an error occurred
            };
            return result;
        }
    }


    public async Task<GetUserInventoryOutput> GetUserInventoryAsync(GetUserInventoryInput input)
    {
        var errorCode = ErrorCode.None;
        var inventory = new List<UserInventoryItem>();

        try
        {
            (errorCode,inventory) = await _gameDb.GetUserInventoryItemAsync(input.AccountId);
            var result = new GetUserInventoryOutput
            {
                AccountId = input.AccountId,
                InventoryItems = inventory,
                ErrorCode = 0 // Assuming 0 means no error
            };
            return result;
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading user inventory for AccountId: {AccountId}", input.AccountId);
            var result = new GetUserInventoryOutput
            {
                AccountId = input.AccountId,
                ErrorCode = ErrorCode.UserInventoryFailException // Assuming -1 means an error occurred
            };
            return result;
        }
    }


    // public async Task<GetUserInfoResult> GetUserInfoAsync(GetUserInfoCommand command)
    // {
    //     try
    //     {
    //         var userInfo = await _gameDb.GetUserInfoAsync(command.AccountId);
    //         return new GetUserInfoResult
    //         {
    //             AccountId = command.AccountId,
    //             Nickname = userInfo.Nickname,
    //             Level = userInfo.Level,
    //             Exp = userInfo.Exp,
    //             ErrorCode = 0 // Assuming 0 means no error
    //         };
    //     }
    //     catch (Exception ex)
    //     {
    //         _logger.LogError(ex, "Error loading user info for AccountId: {AccountId}", command.AccountId);
    //         return new GetUserInfoResult
    //         {
    //             AccountId = command.AccountId,
    //             ErrorCode = -1 // Assuming -1 means an error occurred
    //         };
    //     }
    // }
}
