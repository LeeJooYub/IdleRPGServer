using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GameAPIServer.Models.GameDB;
using GameAPIServer.Models.MasterDB;
using GameAPIServer.Models;

namespace GameAPIServer.DTO.Service;

public class AbilityUpgradeInput
{
    public Int64 PlayerUid { get; set; }
    public int AbilityId { get; set; } // 능력치 ID (예: 1: 공격력, 2: 방어력 등)
    public int Delta { get; set; } // 업그레이드할 값
}

public class AbilityUpgradeOutput : ErrorCodeDTO
{
}

public class StatusUpgradeInput
{
    public Int64 PlayerUid { get; set; }
    public int UpgradeType { get; set; } // 업그레이드 타입 (예: 공격력, 방어력 등)
    public int UpgradeValue { get; set; } // 업그레이드할 값
}

public class StatusUpgradeOutput : ErrorCodeDTO
{
}