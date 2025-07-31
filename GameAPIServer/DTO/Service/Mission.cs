using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GameAPIServer.Models;
using GameAPIServer.Models.MasterDB;
using GameAPIServer.Models.GameDB;

namespace GameAPIServer.DTO.Service;


public class ClearGuideMissionInput
{
    public Int64 PlayerUid { get; set; }
    public int GuideMissionSeq { get; set; }
}

public class ClearGuideMissionOutput : ErrorCodeDTO
{
    public int GuideMissionSeq { get; set; }
    public GuideMission NextGuideMission { get; set; } // 다음 가이드 미션
    public RewardData Reward { get; set; }
}
