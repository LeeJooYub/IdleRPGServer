using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameAPIServer.Models.GameDB;
using GameAPIServer.Models.MasterDB;
using GameAPIServer.Models;
using GameAPIServer.DTO.Service;


using GameAPIServer.Repository.Interfaces;
using GameAPIServer.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GameAPIServer.Services;

public class GrowthService : IGrowthService
{

    private readonly IGameDb _gameDb;
    private readonly IMasterDb _masterDb;
    private readonly ILogger<GrowthService> _logger;

    public GrowthService(IGameDb gameDb, IMasterDb masterDb, ILogger<GrowthService> logger)
    {
        _gameDb = gameDb;
        _masterDb = masterDb;
        _logger = logger;
    }

    public async Task<ErrorCode> UpgradeAbilityAsync(AbilityUpgradeInput input)
    {
        var output = new ErrorCode();
        
        // 캐릭터 능력치 상태 가져오기
        var UserCharacterStatus = await _gameDb.GetUserCharacterStatusAsync(input.PlayerUid);
        if (UserCharacterStatus == null)
        {
            return ErrorCode.GameDbGetUserCharacterStatusError;
        }
        var moneyLeft = (await _gameDb.GetUserCurrencyAsync(input.PlayerUid, 1)).amount;
        

        // 능력치 업데이트 및 비용 지불
        try
        {
            switch (input.AbilityId)
            {
                case 1: // 공격력
                    var ability = await _masterDb.GetAbilityAsync(input.AbilityId);
                    if (ability == null)
                    {
                        return ErrorCode.MasterDbGetAbilityError;
                    }
                    var currCost = ability.init_cost + ability.cost_increment_delta * UserCharacterStatus.character_atk * (UserCharacterStatus.character_atk - 1) / 2;
                    var goalCost = ability.init_cost + ability.cost_increment_delta * (UserCharacterStatus.character_atk + input.Delta) * (UserCharacterStatus.character_atk - 1) / 2;
                    var diffCost = goalCost - currCost;
                    if (moneyLeft < diffCost)
                    {
                        return ErrorCode.InsufficientCurrency;
                    }


                    //일단 돈으로 지불
                    await _gameDb.UpdateUserCurrencyAsync(input.PlayerUid, 1, -diffCost);
                    //능력치 업그레이드
                    await _gameDb.UpdateUserCharacterAtkAsync(input.PlayerUid, input.Delta);
                    break;

                case 2: // 방어력
                    var defAbility = await _masterDb.GetAbilityAsync(input.AbilityId);
                    if (defAbility == null)
                    {
                        return ErrorCode.MasterDbGetAbilityError;
                    }
                    var defCurrCost = defAbility.init_cost + defAbility.cost_increment_delta * UserCharacterStatus.character_def * (UserCharacterStatus.character_def - 1) / 2;
                    var defGoalCost = defAbility.init_cost + defAbility.cost_increment_delta * (UserCharacterStatus.character_def + input.Delta) * (UserCharacterStatus.character_def - 1) / 2;
                    var defDiffCost = defGoalCost - defCurrCost;

                    if (moneyLeft < defDiffCost)
                    {
                        return ErrorCode.InsufficientCurrency;
                    }
                    //일단 돈으로 지불
                    await _gameDb.UpdateUserCurrencyAsync(input.PlayerUid, 1, -defDiffCost);
                    //능력치 업그레이드
                    await _gameDb.UpdateUserCharacterDefAsync(input.PlayerUid, input.Delta);
                    break;
                // Add more cases for other abilities as needed
                case 3: // 체력
                    var hpAbility = await _masterDb.GetAbilityAsync(input.AbilityId);
                    if (hpAbility == null)
                    {
                        return ErrorCode.MasterDbGetAbilityError;
                    }
                    var hpCurrCost = hpAbility.init_cost + hpAbility.cost_increment_delta * UserCharacterStatus.character_hp * (UserCharacterStatus.character_hp - 1) / 2;
                    var hpGoalCost = hpAbility.init_cost + hpAbility.cost_increment_delta * (UserCharacterStatus.character_hp + input.Delta) * (UserCharacterStatus.character_hp - 1) / 2;
                    var hpDiffCost = hpGoalCost - hpCurrCost;

                    if (moneyLeft < hpDiffCost)
                    {
                        return ErrorCode.InsufficientCurrency;
                    }
                    //일단 돈으로 지불
                    await _gameDb.UpdateUserCurrencyAsync(input.PlayerUid, 1, -hpDiffCost);
                    //능력치 업그레이드
                    await _gameDb.UpdateUserCharacterHpAsync(input.PlayerUid, input.Delta);
                    break;
                case 4: // 마나
                    var mpAbility = await _masterDb.GetAbilityAsync(input.AbilityId);
                    if (mpAbility == null)
                    {
                        return ErrorCode.MasterDbGetAbilityError;
                    }
                    var mpCurrCost = mpAbility.init_cost + mpAbility.cost_increment_delta * UserCharacterStatus.character_mp * (UserCharacterStatus.character_mp - 1) / 2;
                    var mpGoalCost = mpAbility.init_cost + mpAbility.cost_increment_delta * (UserCharacterStatus.character_mp + input.Delta) * (UserCharacterStatus.character_mp - 1) / 2;
                    var mpDiffCost = mpGoalCost - mpCurrCost;

                    if (moneyLeft < mpDiffCost)
                    {
                        return ErrorCode.InsufficientCurrency;
                    }
                    //일단 돈으로 지불
                    await _gameDb.UpdateUserCurrencyAsync(input.PlayerUid, 1, -mpDiffCost);
                    //능력치 업그레이드
                    await _gameDb.UpdateUserCharacterMpAsync(input.PlayerUid, input.Delta);
                    break;
                default:
                    return ErrorCode.InvalidAbilityId;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error upgrading ability for PlayerUid: {PlayerUid}, AbilityId: {AbilityId}", input.PlayerUid, input.AbilityId);
            return ErrorCode.DatabaseError;
            // Log the exception or handle it as needed
        }

        return output;
    }

}

