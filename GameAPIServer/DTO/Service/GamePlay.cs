using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GameAPIServer.Models;

namespace GameAPIServer.DTO.Service;

public class StageClearInput
{
    public Int64 AccountUid { get; set; }
    public int StageId { get; set; }
}

public class StageClearOutput : ErrorCodeDTO
{
    public int StageId { get; set; }
    public RewardData Reward { get; set; }
}

public class GuideMissionClearInput
{
    public Int64 AccountUid { get; set; }
    public int GuideMissionId { get; set; }
}

public class GuideMissionClearOutput : ErrorCodeDTO
{
    public RewardData Reward { get; set; }
}
