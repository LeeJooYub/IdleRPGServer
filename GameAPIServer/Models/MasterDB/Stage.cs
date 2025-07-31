using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPIServer.Models.MasterDB;

public class Stage : RewardData
{
    public int stage_id { get; set; } // 스테이지 ID. 백의 자리 숫자는 스테이지 챕터, 0~99까지는 해당 챕터 스테이지
    public char boss_stage_yn { get; set; } // 보스 여부 (Y: 보스, N: 일반 스테이지)
}