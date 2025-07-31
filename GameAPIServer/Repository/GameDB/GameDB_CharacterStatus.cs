using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameAPIServer.Models.GameDB;

using SqlKata.Execution;

using GameAPIServer.Repository.Interfaces;

namespace GameAPIServer.Repository;

public partial class GameDb : IGameDb
{
    public async Task<ErrorCode> UpdateUserCharacterStatusAsync(Int64 PlayerUid, UserCharacterStatus status)
    {
        // Update the user's current stage progress in the database
        var result = 1;
        return result > 0 ? ErrorCode.None : ErrorCode.DatabaseError; // Return 0 for success, -1 for failure
    }

    public async Task<UserCharacterStatus> GetUserCharacterStatusAsync(Int64 PlayerUid)
    {
        // Retrieve the user's character status from the database
        var query = _queryFactory.Query("user_character_status")
            .Where("player_uid", PlayerUid)
            .FirstOrDefaultAsync<UserCharacterStatus>();
        return await query;
    }

    public async Task<ErrorCode> UpdateUserCharacterAtkAsync(Int64 PlayerUid, int delta)
    {
        // Update the user's character attack in the database
        var result = await _queryFactory.Query("user_character_status")
            .Where("player_uid", PlayerUid)
            .IncrementAsync("character_atk", delta);
        if (result < 0)
        {
            return ErrorCode.DatabaseError;
        }
        return ErrorCode.None; 
    }

    public async Task<ErrorCode> UpdateUserCharacterDefAsync(Int64 PlayerUid, int delta)
    {
        // Update the user's character defense in the database
        var result = await _queryFactory.Query("user_character_status")
            .Where("player_uid", PlayerUid)
            .IncrementAsync("character_def", delta);
        if (result < 0)
        {
            return ErrorCode.DatabaseError;
        }
        return ErrorCode.None;
    }

    public async Task<ErrorCode> UpdateUserCharacterHpAsync(Int64 PlayerUid, int delta)
    {
        // Update the user's character health in the database
        var result = await _queryFactory.Query("user_character_status")
            .Where("player_uid", PlayerUid)
            .IncrementAsync("character_hp", delta);
        if (result < 0)
        {
            return ErrorCode.DatabaseError;
        }
        return ErrorCode.None; 
    }

    public async Task<ErrorCode> UpdateUserCharacterMpAsync(Int64 PlayerUid, int delta)
    {
        // Update the user's character mana in the database
        var result = await _queryFactory.Query("user_character_status")
            .Where("player_uid", PlayerUid)
            .IncrementAsync("character_mp", delta);
        if (result < 0)
        {
            return ErrorCode.DatabaseError;
        }
        return ErrorCode.None;
    }

}
