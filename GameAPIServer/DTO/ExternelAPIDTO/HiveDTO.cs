using System;
using System.ComponentModel.DataAnnotations;

namespace GameAPIServer.DTO.ExternelAPI;

public class HiveVerifyTokenResponse
{
    [Required]
    public ErrorCode ErrorCode { get; set; } = ErrorCode.None;
}