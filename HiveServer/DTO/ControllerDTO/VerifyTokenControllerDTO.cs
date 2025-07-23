using System;
using System.ComponentModel.DataAnnotations;

namespace HiveServer.Model.DTO;

public class VerifyTokenRequest
{
    [Required]
    public string HiveToken { get; set; }
    [Required]
    public Int64 AccountId { get; set; }
}

public class VerifyTokenResponse
{
    [Required]
    public ErrorCode Result { get; set; } = ErrorCode.None;
}

