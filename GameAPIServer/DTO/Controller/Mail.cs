using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

using GameAPIServer.Models.GameDB;
using GameAPIServer.Models.MasterDB;
using GameAPIServer.Models;

namespace GameAPIServer.DTO.ControllerDTO;

public class MailListRequest
{
    public DateTime? Cursor { get; set; } // 커서 값 (마지막 메일의 receive_dt) (default: 현재 시간)
    public int Limit { get; set; } = 10; // 가져올 메일 개수
}

public class MailListResponse : ErrorCodeDTO
{
    public List<Mail> Mails { get; set; } // 메일 목록
    public DateTime? NextCursor { get; set; } // 다음 페이지를 위한 커서 값 (마지막 메일의 receive_dt)
}

public class ReceiveMailRequest
{
    [Required] public Int64 MailId { get; set; } // 메일 ID
}

public class ReceiveMailResponse : ErrorCodeDTO
{
    public RewardData Reward { get; set; } // 보상 목록
}

public class DeleteMailRequest
{
    [Required] public Int64 MailId { get; set; } // 메일 ID
}

public class DeleteMailResponse : ErrorCodeDTO
{
}

public class ReceiveAllMailsRequest
{
    [Required] public DateTime? Now { get; set; } = DateTime.UtcNow; // 현재 시간 (기본값: 현재 시간)
}

public class ReceiveAllMailsResponse : ErrorCodeDTO
{
    public int TotalClaimed { get; set; } = 0; // 수령한 메일 수
    public List<RewardData> Rewards { get; set; } = new List<RewardData>(); // 보상 목록
}

