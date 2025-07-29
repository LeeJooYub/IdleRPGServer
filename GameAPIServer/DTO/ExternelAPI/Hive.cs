using System;
using System.ComponentModel.DataAnnotations;

namespace GameAPIServer.DTO.ExternelAPI;

public class HiveVerifyTokenResponse
{
    public ErrorCode Result { get; set; } = ErrorCode.None;
}