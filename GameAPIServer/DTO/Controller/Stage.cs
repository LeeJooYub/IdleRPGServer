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
    public Stage ClearedStage { get; set; }
    public Stage NextStage { get; set; } // 다음 스테이지
    public RewardData Reward { get; set; } // 보상 데이터
}


