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
    // Reward 관련 메소드
    public async Task<Ability> GetAbilityAsync(int abilityId)
    {
        var query = _queryFactory.Query("ability")
            .Where("ability_id", abilityId)
            .FirstAsync<Ability>();
        var ability = await query;
        return ability;
    }
       
}