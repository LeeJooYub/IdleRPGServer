using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using GameAPIServer.Models.GameDB;
using GameAPIServer.DTO.ControllerDTO;
using GameAPIServer.Models.MasterDB;

namespace GameAPIServer.DTO.ServiceDTO
{
    // GetMailList
    public class MailListServiceInput
    {
        public Int64 AccountId { get; set; } // 사용자 ID
        public DateTime? Cursor { get; set; } // 커서 값 (마지막 메일 ID)
        public int Limit { get; set; } // 가져올 메일 개수
    }

    public class MailListServiceOutput : ErrorCodeDTO
    {
        public List<Mail> Mails { get; set; } // 메일 목록
        public DateTime? NextCursor { get; set; } // 다음 페이지를 위한 커서 값 (마지막 메일의 receive_dt)
    }

    // GetMailDetail
    // public class MailDetailCommand
    // {
    //     public Int64 MailId { get; set; } // 메일 ID
    // }

    // public class MailDetailResult
    // {
    //     [Required] public ErrorCode ErrorCode { get; set; } = ErrorCode.None; // 에러 코드
    //     public string Sender { get; set; } // 발신자
    //     public string Content { get; set; } // 메일 내용
    //     public DateTime SentDate { get; set; } // 발송 날짜
    // }

    // ClaimMail
    public class ClaimMailServiceInput
    {
        public Int64 MailId { get; set; } // 메일 ID
        public Int64 AccountId { get; set; } // 사용자 ID
    }

    public class ClaimMailServiceOutput : ErrorCodeDTO
    {
        public List<MailRewardDto> Rewards { get; set; } // 보상 목록
    }


    // DeleteMail
    public class DeleteMailServiceInput
    {
        public Int64 MailId { get; set; } // 메일 ID
        public Int64 AccountId { get; set; } // 사용자 ID
    }

    public class DeleteMailServiceOutput : ErrorCodeDTO
    {
    }

    // ClaimAllMails
    // public class ClaimAllMailsCommand
    // {
    //     public Int64 AccountId { get; set; } // 사용자 ID
    // }

    // public class ClaimAllMailsResult : ErrorCodeDTO
    // {
    //     public int TotalClaimed { get; set; } = 0;// 수령한 메일 수
    //     public List<MailRewardDto> Rewards { get; set; } // 보상 목록
    // }

    
}
