using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GameAPIServer.Models;
using GameAPIServer.Models.MasterDB;

namespace GameAPIServer.DTO.Controller;

public class GuideMissionClearRequest
{
    public int GuideMissionSeq { get; set; }
}

public class GuideMissionClearResponse : ErrorCodeDTO
{
    public GuideMission NextGuideMission { get; set; } // 다음 가이드 미션
    public RewardData Reward { get; set; } // 보상 데이터
}
