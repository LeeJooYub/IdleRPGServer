using System;
using System.ComponentModel.DataAnnotations;

namespace HiveServer.Model.DTO;

public class VerifyTokenCommand
{
    [Required]
    public string HiveToken { get; set; }
    [Required]
    public Int64 PlayerId { get; set; }
}

public class VerifyTokenResult
{
    [Required]
    public ErrorCode Result { get; set; } = ErrorCode.None;
}

