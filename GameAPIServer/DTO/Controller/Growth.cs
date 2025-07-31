using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPIServer.DTO.Controller;

public class AbilityUpgradeRequest
{
    public int AbilityId { get; set; } // 업그레이드 대상 능력치 ID (1: ATK, 2: DEF, 3: CRIT 등)
    public int Delta { get; set; } // 얼마나 업그레이드 할 것인지
}
public class AbilityUpgradeResponse : ErrorCodeDTO
{
}

public class StatusUpgradeRequest
{
    public int UpgradeId { get; set; } // 업그레이드 ID
    public int UpgradeLevel { get; set; } // 업그레이드 레벨
}

public class StatusUpgradeResponse : ErrorCodeDTO
{
}