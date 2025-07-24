using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using GameAPIServer.Models.GameDB;
using GameAPIServer.Models.MasterDB;

namespace GameAPIServer.DTO.ServiceDTO
{
    // GetMailList
    public class MailListCommand
    {
        public Int64 AccountId { get; set; } // 사용자 ID
        public DateTime? Cursor { get; set; } // 커서 값 (마지막 메일 ID)
        public int Limit { get; set; } // 가져올 메일 개수
    }

    public class MailListResult
    {
        [Required] public ErrorCode ErrorCode { get; set; } = ErrorCode.None; // 에러 코드
        public List<Mail> Mails { get; set; } // 메일 목록
    }

    // GetMailDetail
    public class MailDetailCommand
    {
        public Int64 MailId { get; set; } // 메일 ID
    }

    public class MailDetailResult
    {
        [Required] public ErrorCode ErrorCode { get; set; } = ErrorCode.None; // 에러 코드
        public string Sender { get; set; } // 발신자
        public string Content { get; set; } // 메일 내용
        public DateTime SentDate { get; set; } // 발송 날짜
    }

    // ClaimMail
    public class ClaimMailCommand
    {
        public Int64 MailId { get; set; } // 메일 ID

        public Int64 AccountId { get; set; } // 사용자 ID
    }

    public class ClaimMailResult
    {
        [Required] public ErrorCode ErrorCode { get; set; } = ErrorCode.None; // 에러 코드
        public RewardInMail Reward { get; set; } // 보상 내용
    }

    // DeleteMail
    public class DeleteMailCommand
    {
        public Int64 MailId { get; set; } // 메일 ID
    }

    public class DeleteMailResult
    {
        [Required] public ErrorCode ErrorCode { get; set; } = ErrorCode.None; // 에러 코드
    }

    // ClaimAllMails
    public class ClaimAllMailsCommand
    {
        public Int64 AccountId { get; set; } // 사용자 ID
    }

    public class ClaimAllMailsResult
    {
        [Required] public ErrorCode ErrorCode { get; set; } = ErrorCode.None; // 에러 코드

        public int TotalClaimed { get; set; } // 수령한 메일 수
        public List<string> Rewards { get; set; } // 수령한 보상 목록
    }
}
