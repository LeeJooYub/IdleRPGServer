using System;
using System.ComponentModel.DataAnnotations;

namespace GameAPIServer.DTO.ServiceDTO;

public class LoginServiceInput
{
    [Required]
    public Int64 PlatformId { get; set; } = 0;

    [Required]
    public string PlatformToken { get; set; } = "";

    [Required]
    public string PlatformName { get; set; } = "";
}

public class LoginServiceOutput : ErrorCodeDTO
{
    [Required] public string GameServerToken { get; set; } = "";
    [Required] public Int64 AccountId { get; set; } = 0;

    // public DataLoadUserInfo userData { get; set; }
}