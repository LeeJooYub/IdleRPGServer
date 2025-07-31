using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameAPIServer.Models;
using GameAPIServer.Repository.Interfaces;
using GameAPIServer.Models.MasterDB;

using SqlKata.Execution;

namespace GameAPIServer.Repository;

public partial class MasterDb : IMasterDb
{

    public async Task<RewardData> GetGuideMissionRewardAsync(int guideMissionSeq)
    {
        var query = _queryFactory.Query("guide_mission")
            .Where("guide_mission_seq", guideMissionSeq)
            .FirstAsync<GuideMission>();
        var guideMissionReward = await query;

        var rewardData = new RewardData
        {
            reward_type_cd = guideMissionReward?.reward_type_cd,
            reward_id = guideMissionReward?.reward_id,
            reward_qty = guideMissionReward?.reward_qty
        };

        return rewardData;
    }
}