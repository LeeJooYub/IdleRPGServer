using System;
using System.ComponentModel.DataAnnotations;

namespace GameAPIServer.DTO.ControllerDTO;

public class LoginRequest 
{
    [Required]
    public Int64 AccountId { get; set; } = 0;

    [Required]
    public string HiveToken { get; set; } = "";
}

public class LoginResponse : ErrorCodeDTO
{
    [Required] public string SessionKey { get; set; } = "";
}

public class LogoutRequest
{
    [Required]
    public string SessionKey { get; set; }
}

public class LogoutResponse  : ErrorCodeDTO
{
}