using System;
using System.ComponentModel.DataAnnotations;

namespace HiveServer.Model.DTO;

public class VerifyTokenInput
{
    [Required]
    public string Token { get; set; }
    [Required]
    public Int64 AccountUid { get; set; }
}

public class VerifyTokenOutput
{
    [Required]
    public ErrorCode Result { get; set; } = ErrorCode.None;
}

