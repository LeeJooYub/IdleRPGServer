using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using GameAPIServer.Models.GameDB;
using GameAPIServer.Models.MasterDB;
using GameAPIServer.Models;
using GameAPIServer.DTO.Service;

namespace GameAPIServer.Services.Interfaces
{
    public interface IGrowthService
    {
        public Task<ErrorCode> UpgradeAbilityAsync(AbilityUpgradeInput input);

    }
}
