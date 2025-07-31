using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPIServer.Models.GameDB;

public class UserCharacterProgress
{
    public Int64 player_uid { get; set; } // 사용자 계정 ID
    public int current_stage_id { get; set; } = 0; // 현재 진행 중인 스테이지 ID
    public int current_guide_mission_seq { get; set; } = 0; // 현재 진행 중인 가이드 미션 시퀀스
}
