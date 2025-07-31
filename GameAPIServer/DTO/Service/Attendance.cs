using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GameAPIServer.Models;

namespace GameAPIServer.DTO.Service;

public class CheckTodayInput
{
    public Int64 PlayerUid { get; set; }
    public int AttendanceBookId { get; set; }
    public int CheckNthDay { get; set; } // 출석부의 n번째 날을 체크할 때 사용
    public DateTime Now { get; set; } // 출석체크 유효성 검증 기준 시간
}

public class CheckTodayOutput : ErrorCodeDTO
{
    public RewardData Reward { get; set; } // 보상 데이터
}
