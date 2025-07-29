using System;
using System.ComponentModel.DataAnnotations;

namespace GameAPIServer.DTO.ServiceDTO;

public class LoginInput
{
    [Required]
    public Int64 AccountId { get; set; } = 0;

    [Required]
    public string Token { get; set; } = "";
}

public class LoginOutput : ErrorCodeDTO
{
    [Required] public string GameServerToken { get; set; } = "";
    [Required] public Int64 AccountId { get; set; } = 0;

    // public DataLoadUserInfo userData { get; set; }
}