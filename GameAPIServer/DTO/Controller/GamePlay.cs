using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GameAPIServer.Models;
using GameAPIServer.Models.MasterDB;

namespace GameAPIServer.DTO.Controller;

public class StageClearRequest
{
    [Required]
    public int StageId { get; set; }
}

public class StageClearResponse : ErrorCodeDTO
{
    public Stage StageId { get; set; }
    public Stage NextStageId { get; set; } // 다음 스테이지 ID
    public RewardData Reward { get; set; } // 보상 데이터
}

public class GuideMissionClearRequest
{
    public int GuideMissionSeq { get; set; }
}

public class GuideMissionClearResponse : ErrorCodeDTO
{
    public GuideMission GuideMissionSeq { get; set; }
    public GuideMission NextGuideMissionSeq { get; set; } // 다음 가이드 미션 시퀀스
    public RewardData Reward { get; set; } // 보상 데이터
}
