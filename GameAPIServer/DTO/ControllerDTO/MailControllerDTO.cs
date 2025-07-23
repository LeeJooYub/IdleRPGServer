using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using GameAPIServer.Model.GameDB;

namespace GameAPIServer.DTO.ControllerDTO;

public class MailListRequest
{
    public int AccountId { get; set; } // 사용자 ID
}

public class MailListResponse
{
    public List<string> Mails { get; set; } // 메일 목록
    public int UnreadCount { get; set; } // 읽지 않은 메일 수
}

public class MailDetailRequest
{
    public int MailId { get; set; } // 메일 ID
}

public class MailDetailResponse
{
    public string Sender { get; set; } // 발신자
    public string Content { get; set; } // 메일 내용
    public DateTime SentDate { get; set; } // 발송 날짜
}

public class ClaimMailRequest
{
    public int MailId { get; set; } // 메일 ID
}

public class ClaimMailResponse
{
    public bool IsSuccess { get; set; } // 성공 여부
    public string Reward { get; set; } // 보상 내용
}

public class DeleteMailRequest
{
    public int MailId { get; set; } // 메일 ID
}

public class DeleteMailResponse
{
    public bool IsDeleted { get; set; } // 삭제 성공 여부
}

public class ClaimAllMailsRequest
{
    public int UserId { get; set; } // 사용자 ID
}

public class ClaimAllMailsResponse
{
    public int TotalClaimed { get; set; } // 수령한 메일 수
    public List<string> Rewards { get; set; } // 수령한 보상 목록
}
