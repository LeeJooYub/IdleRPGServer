using System;

namespace HiveServer.Model.Entity;

public class AccountInfo
{
    public Int64 PlayerId { get; set; }
    public string Email { get; set; }
    public string Pw { get; set; }
    public string SaltValue { get; set; }
    public string CreateDt { get; set; }
}
