using System;
using System.ComponentModel.DataAnnotations;

namespace GameAPIServer.DTO.ControllerDTO;

public class LoginRequest
{
    [Required]
    public Int64 PlatformId { get; set; } = 0;

    [Required]
    public string PlatformToken { get; set; } = "";

    [Required]
    public string PlatformName { get; set; } = "";
}

public class LoginResponse
{
    [Required] public ErrorCode ErrorCode { get; set; } = ErrorCode.None;
    [Required] public string GameServerToken { get; set; } = "";
    [Required] public Int64 AccountId { get; set; } = 0;

    // public DataLoadUserInfo userData { get; set; }
}

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