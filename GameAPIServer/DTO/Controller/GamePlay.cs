using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GameAPIServer.Models;

namespace GameAPIServer.DTO.Controller;

public class StageClearRequest
{
    [Required]
    public int StageId { get; set; }
    public Int64 AccountUid { get; set; }
}

public class StageClearResponse : ErrorCodeDTO
{
    public int StageId { get; set; }
    public RewardData Reward { get; set; } // 보상 데이터
}

public class GuideMissionClearRequest
{
    public int GuideMissionId { get; set; }
    public Int64 AccountUid { get; set; }
}

public class GuideMissionClearResponse : ErrorCodeDTO
{
    public int GuideMissionId { get; set; }
    public RewardData Reward { get; set; } // 보상 데이터
}
