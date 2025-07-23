using System;
using System.Collections.Generic;

using GameAPIServer.Model.GameDB;

namespace GameAPIServer.DTO.ServiceDTO
{
    // GetMailList
    public class MailListCommand
    {
        public int AccountId { get; set; } // 사용자 ID
    }

    public class MailListResult
    {
        public List<string> Mails { get; set; } // 메일 목록
        public int UnreadCount { get; set; } // 읽지 않은 메일 수
    }

    // GetMailDetail
    public class MailDetailCommand
    {
        public int MailId { get; set; } // 메일 ID
    }

    public class MailDetailResult
    {
        public string Sender { get; set; } // 발신자
        public string Content { get; set; } // 메일 내용
        public DateTime SentDate { get; set; } // 발송 날짜
    }

    // ClaimMail
    public class ClaimMailCommand
    {
        public int MailId { get; set; } // 메일 ID
    }

    public class ClaimMailResult
    {
        public bool IsSuccess { get; set; } // 성공 여부
        public string Reward { get; set; } // 보상 내용
    }

    // DeleteMail
    public class DeleteMailCommand
    {
        public int MailId { get; set; } // 메일 ID
    }

    public class DeleteMailResult
    {
        public bool IsDeleted { get; set; } // 삭제 성공 여부
    }

    // ClaimAllMails
    public class ClaimAllMailsCommand
    {
        public int AccountId { get; set; } // 사용자 ID
    }

    public class ClaimAllMailsResult
    {
        public int TotalClaimed { get; set; } // 수령한 메일 수
        public List<string> Rewards { get; set; } // 수령한 보상 목록
    }
}
