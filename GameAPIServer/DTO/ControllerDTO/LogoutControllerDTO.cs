using System;
using System.ComponentModel.DataAnnotations;

namespace GameAPIServer.DTO.ControllerDTO;

public class LogoutRequest
{
    [Required]
    public Int64 AccountId { get; set; }

    [Required]
    public string GameServerToken { get; set; }
}

public class LogoutResponse
{
    [Required] public ErrorCode ErrorCode { get; set; } = ErrorCode.None;
}

