using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

using GameAPIServer.Models.GameDB;
using GameAPIServer.Models.MasterDB;

namespace GameAPIServer.DTO.ControllerDTO;

public class MailListRequest
{
    public Int64 AccountId { get; set; } // 사용자 ID
    public DateTime? Cursor { get; set; } // 커서 값 (마지막 메일의 receive_dt)
    public int Limit { get; set; } // 가져올 메일 개수
}

public class MailListResponse
{
    public List<Mail> Mails { get; set; } // 메일 목록
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
    public Int64 MailId { get; set; } // 메일 ID

    public Int64 AccountId { get; set; } // 사용자 ID
}

public class ClaimMailResponse
{
    [Required] public ErrorCode ErrorCode { get; set; } = ErrorCode.None;
    public RewardInMail Reward { get; set; } // 보상 내용
}

public class DeleteMailRequest
{
    public Int64 MailId { get; set; } // 메일 ID
}

public class DeleteMailResponse
{
    [Required] public ErrorCode ErrorCode { get; set; } = ErrorCode.None;
}

public class ClaimAllMailsRequest
{
    public Int64 AccountId { get; set; } // 사용자 ID
}

public class ClaimAllMailsResponse
{
    [Required] public ErrorCode ErrorCode { get; set; } = ErrorCode.None;
    public int TotalClaimed { get; set; } // 수령한 메일 수
    public List<string> Rewards { get; set; } // 수령한 보상 목록
}
