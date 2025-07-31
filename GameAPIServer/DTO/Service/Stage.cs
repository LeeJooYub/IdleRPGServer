using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GameAPIServer.Models;
using GameAPIServer.Models.MasterDB;
using GameAPIServer.Models.GameDB;

namespace GameAPIServer.DTO.Service;

public class ClearStageInput
{
    public Int64 PlayerUid { get; set; }
    public int StageId { get; set; }
}

public class ClearStageOutput : ErrorCodeDTO
{
    public Stage ClearedStage { get; set; }
    public Stage NextStage { get; set; } // 다음 스테이지
    public RewardData Reward { get; set; }
}
