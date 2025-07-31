using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameAPIServer.Models;
using GameAPIServer.Repository.Interfaces;
using GameAPIServer.Models.MasterDB;
using GameAPIServer.Models.GameDB;

using SqlKata.Execution;

namespace GameAPIServer.Repository;

public partial class MasterDb : IMasterDb
{
    // 스테이지 클리어 보상 관련 메소드
    public async Task<RewardData> GetStageClearRewardAsync(int stageId)
    {
        var query = _queryFactory.Query("stage")
            .Where("stage_id", stageId)
            .FirstAsync<Stage>();
        var stageClearReward = await query;

        var rewardData = new RewardData
        {
            reward_type_cd = stageClearReward?.reward_type_cd,
            reward_id = stageClearReward?.reward_id,
            reward_qty = stageClearReward?.reward_qty
        };

        return rewardData;
    }

    public async Task<Stage> GetStageAsync(int stageId)
    {
        var query = _queryFactory.Query("stage")
            .Where("stage_id", stageId)
            .FirstOrDefaultAsync<Stage>();
        var stage = await query;

        return stage;
    }

}