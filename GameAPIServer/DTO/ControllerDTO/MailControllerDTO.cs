using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

using GameAPIServer.Models.GameDB;
using GameAPIServer.Models.MasterDB;

namespace GameAPIServer.DTO.ControllerDTO;

public class MailListRequest
{
    [Required] public Int64 AccountId { get; set; } // 사용자 ID
    public DateTime? Cursor { get; set; } // 커서 값 (마지막 메일의 receive_dt) (default: 현재 시간)
    public int Limit { get; set; } // 가져올 메일 개수
}

public class MailListResponse
{
    public List<Mail> Mails { get; set; } // 메일 목록
    public DateTime? NextCursor { get; set; } // 다음 페이지를 위한 커서 값 (마지막 메일의 receive_dt)
    public ErrorCode ErrorCode { get; set; } = ErrorCode.None; // 에러 코드
}

// public class MailDetailRequest
// {
//     public int MailId { get; set; } // 메일 ID
// }

// public class MailDetailResponse
// {
//     public string Sender { get; set; } // 발신자
//     public string Content { get; set; } // 메일 내용
//     public DateTime SentDate { get; set; } // 발송 날짜
// }

public class ClaimMailRequest
{
    [Required] public Int64 MailId { get; set; } // 메일 ID
    [Required] public Int64 AccountId { get; set; } // 사용자 ID
}

public class ClaimMailResponse
{
    public ErrorCode ErrorCode { get; set; } = ErrorCode.None;
    public List<MailRewardDto> Rewards { get; set; } // 보상 목록
}

public class DeleteMailRequest
{
    [Required] public Int64 MailId { get; set; } // 메일 ID
    [Required] public Int64 AccountId { get; set; } // 사용자 ID
}

public class DeleteMailResponse
{
    public ErrorCode ErrorCode { get; set; } = ErrorCode.None;
}

public class ClaimAllMailsRequest
{
    [Required] public Int64 AccountId { get; set; } // 사용자 ID
}

public class ClaimAllMailsResponse
{
    public ErrorCode ErrorCode { get; set; } = ErrorCode.None;
    public int TotalClaimed { get; set; } // 수령한 메일 수
    public List<MailRewardDto> Rewards { get; set; } // 보상 목록
}

public class MailRewardDto
{
    public int? RewardId { get; set; }
    public string RewardType { get; set; }
    public int? RewardQty { get; set; }
}
