using System;
using System.ComponentModel.DataAnnotations;

namespace GameAPIServer.DTO.Service;

public class LoginInput
{
    [Required]
    public Int64 PlayerUid { get; set; } = 0;

    [Required]
    public string Token { get; set; } = "";
}

public class LoginOutput : ErrorCodeDTO
{
    [Required] public string Token { get; set; } = "";
    // public DataLoadUserInfo userData { get; set; }
}