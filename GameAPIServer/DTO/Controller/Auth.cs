using System;
using System.ComponentModel.DataAnnotations;

namespace GameAPIServer.DTO.Controller;

public class LoginRequest 
{
    [Required]
    public Int64 PlayerUid { get; set; } = 0;

    [Required]
    public string Token { get; set; } = "";
}

public class LoginResponse : ErrorCodeDTO
{
}

public class LogoutRequest
{
    [Required]
    public string Token { get; set; }
}

public class LogoutResponse  : ErrorCodeDTO
{
}