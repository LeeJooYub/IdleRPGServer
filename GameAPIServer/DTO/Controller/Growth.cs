using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPIServer.DTO.Controller;

public class AbilityUpgradeRequest
{
    public int AbilityId { get; set; } // 능력치 업그레이드 ID
    public int UpgradeLevel { get; set; } // 업그레이드 레벨
}
public class AbilityUpgradeResponse : ErrorCodeDTO
{
    public string AbilityName { get; set; } // 능력치 이름
    public int AfterUpgradeLevel { get; set; } // 업그레이드 이후 레벨
}

public class StatusUpgradeRequest
{
    public int UpgradeId { get; set; } // 업그레이드 ID
    public int UpgradeLevel { get; set; } // 업그레이드 레벨
}

public class StatusUpgradeResponse : ErrorCodeDTO
{
    public int UpgradeId { get; set; } // 업그레이드 ID
    public int UpgradeLevel { get; set; } // 업그레이드 레벨
}