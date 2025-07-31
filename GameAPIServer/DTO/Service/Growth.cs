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
    public int AbilityType { get; set; } // 능력치 타입 (예: 공격력, 방어력 등)
    public int UpgradeValue { get; set; } // 업그레이드할 값
    public DateTime Now { get; set; } // 업그레이드 유효성 검증 기준 시간
}

public class AbilityUpgradeOutput : ErrorCodeDTO
{
    public string AbilityName { get; set; } // 능력치 이름
    public int AfterUpgradeLevel { get; set; } // 업그레이드 이후 레벨
}

public class StatusUpgradeInput
{
    public Int64 PlayerUid { get; set; }
    public int UpgradeType { get; set; } // 업그레이드 타입 (예: 공격력, 방어력 등)
    public int UpgradeValue { get; set; } // 업그레이드할 값
    public DateTime Now { get; set; } // 업그레이드 유효성 검증 기준 시간
}

public class StatusUpgradeOutput : ErrorCodeDTO
{
    public int UpgradeId { get; set; } // 업그레이드 ID
    public int UpgradeLevel { get; set; } // 업그레이드 레벨
    public List<RewardData> Rewards { get; set; } // 보상 데이터 리스트
}