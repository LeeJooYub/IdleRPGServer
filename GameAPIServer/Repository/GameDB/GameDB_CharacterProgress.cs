using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GameAPIServer.Repository.Interfaces;
using GameAPIServer.Models.GameDB;
using SqlKata.Execution;

namespace GameAPIServer.Repository;

public partial class GameDb : IGameDb
{
    public async Task<ErrorCode> UpdateUserCharacterStageProgressAsync(Int64 PlayerUid, int stageId)
    {
        var affectedRows = await _queryFactory.Query("user_character_progress")
            .Where("player_uid", PlayerUid)
            .UpdateAsync(new
            {
                current_stage_id = stageId,
            });
        if (affectedRows == 0)
        {
            return ErrorCode.DatabaseError; // 해당 스테이지가 존재하지 않음
        }

        return ErrorCode.None;
    }

    public async Task<ErrorCode> UpdateUserCharacterGuideMissionProgressAsync(Int64 PlayerUid, int guideMissionSeq)
    {
        var affectedRows = await _queryFactory.Query("user_character_progress")
            .Where("player_uid", PlayerUid)
            .UpdateAsync(new
            {
                current_guide_mission_seq = guideMissionSeq,
            });
        if (affectedRows == 0)
        {
            return ErrorCode.DatabaseError; // 해당 가이드 미션이 존재하지 않음
        }

        return ErrorCode.None;
    }

    public async Task<UserCharacterProgress> GetUserCharacterProgressAsync(Int64 PlayerUid)
    {
        var userCharacterProgress = await _queryFactory.Query("user_character_progress")
            .Where("player_uid", PlayerUid)
            .FirstAsync<UserCharacterProgress>();



        return userCharacterProgress;
    }

}

