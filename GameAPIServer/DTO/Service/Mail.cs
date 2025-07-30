using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using GameAPIServer.Models.GameDB;
using GameAPIServer.DTO.ControllerDTO;
using GameAPIServer.Models.MasterDB;
using GameAPIServer.Models;

namespace GameAPIServer.DTO.ServiceDTO
{
    // GetMailList
    public class MailListInput
    {
        public Int64 AccountUid { get; set; } // 사용자 ID
        public DateTime? Cursor { get; set; } // 커서 값 (마지막 메일 ID)
        public int Limit { get; set; } // 가져올 메일 개수
    }

    public class MailListOutput : ErrorCodeDTO
    {
        public List<Mail> Mails { get; set; } // 메일 목록
        public DateTime? NextCursor { get; set; } // 다음 페이지를 위한 커서 값 (마지막 메일의 receive_dt)
    }



    // ClaimMail
    public class ReceiveMailInput
    {
        public Int64 MailId { get; set; } // 메일 ID
        public Int64 AccountUid { get; set; } // 사용자 ID
    }

    public class ReceiveMailOutput : ErrorCodeDTO
    {
        public RewardData Reward { get; set; } // 보상 목록
    }


    // ClaimMail
    public class ReceiveAllMailInput
    {
        public Int64 MailId { get; set; } // 메일 ID
        public Int64 AccountUid { get; set; } // 사용자 ID
        public DateTime? Now { get; set; } // 현재 시간 (기본값: 현재 시간)
    }

    public class ReceiveAllMailOutput : ErrorCodeDTO
    {
        public List<RewardData> Rewards { get; set; } // 보상 목록
    }



    // DeleteMail
    public class DeleteMailInput
    {
        public Int64 MailId { get; set; } // 메일 ID
        public Int64 AccountUid { get; set; } // 사용자 ID
    }

    public class DeleteMailOutput : ErrorCodeDTO
    {
    }
    
}
