using System;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using MySqlConnector;
using SqlKata.Execution;
using SqlKata.Extensions;

using GameAPIServer.Repository.Interfaces;
using GameAPIServer.Models.GameDB;
using GameAPIServer.Models.MasterDB;
using GameAPIServer.DTO.ServiceDTO;
using GameAPIServer.DTO.ControllerDTO;
using System.Linq;
using System.Runtime.CompilerServices;


namespace GameAPIServer.Repository;

public partial class GameDb : IGameDb
{
    public async Task<(ErrorCode,List<Currency>)> GetUserCurrencyAsync(Int64 accountId)
    {
        ErrorCode errorCode = ErrorCode.None;
        List<Currency> currencyList = new List<Currency>();

        try
        {
            SqlKata.Query query = _queryFactory.Query("user_currency")
                .Where("account_id", accountId);

            UserCurrency userCurrency = await query.FirstOrDefaultAsync<UserCurrency>();

            if (userCurrency == null)
            {
                errorCode = ErrorCode.UserMoneyInfoFailException;
                return (errorCode, currencyList);
            }

            return (errorCode, userCurrency.CurrencyList);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user currency for AccountId: {AccountId}", accountId);
            errorCode = ErrorCode.DatabaseError;
            return (errorCode, currencyList);
        }
    }

    public async Task<(ErrorCode, List<UserInventory>)> GetUserInventoryItemAsync(Int64 accountId)
    {
        ErrorCode errorCode = ErrorCode.None;
        List<UserInventory> userInventory = new List<UserInventory>();
        try
        {
            SqlKata.Query query = _queryFactory.Query("user_inventory")
                .Where("account_id", accountId);
            userInventory = await query.FirstOrDefaultAsync<List<UserInventory>>();

            return (errorCode, userInventory);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user inventory for AccountId: {AccountId}", accountId);
            errorCode = ErrorCode.DatabaseError;
            return (errorCode, userInventory);
        }
    }



    // public async Task<GetUserCharacterInfoResult> GetUserCharacterInfoAsync(GetUserCharacterInfoCommand command)
    // {
    //     try
    //     {
    //         // Implement the logic to retrieve user character info
    //         // This is a placeholder implementation
    //         var characterInfo = new CharacterInfo
    //         {
    //             AccountId = command.AccountId,
    //             Level = 1,
    //             Experience = 0,
    //             Skills = new List<Skill>()
    //         };

    //         return new GetUserCharacterInfoResult
    //         {
    //             AccountId = command.AccountId,
    //             CharacterInfo = characterInfo,
    //             ErrorCode = 0 // Assuming 0 means no error
    //         };
    //     }
    //     catch (Exception ex)
    //     {
    //         _logger.LogError(ex, "Error loading user character info for AccountId: {AccountId}", command.AccountId);
    //         return new GetUserCharacterInfoResult
    //         {
    //             AccountId = command.AccountId,
    //             CharacterInfo = null, // Return null on error
    //             ErrorCode = -1 // Assuming -1 means an error occurred
    //         };
    //     }
    // }


}
