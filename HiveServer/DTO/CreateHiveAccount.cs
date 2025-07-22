using System.ComponentModel.DataAnnotations;

namespace HiveServer.Model.DTO;

public class CreateHiveAccountRequest
{
    [Required]
    [MinLength(1, ErrorMessage = "EMAIL CONNOT BE EMPTY")]
    [MaxLength(50, ErrorMessage = "EMAIL IS TOO LONG")]
    [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
    public string Email { get; set; }

    [Required]
    [MinLength(1, ErrorMessage = "EMAIL CONNOT BE EMPTY")]
    [MaxLength(30, ErrorMessage = "EMAIL IS TOO LONG")]
    [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}

public class CreateHiveAccountResponse
{
    [Required]
    public ErrorCode Result { get; set; } = ErrorCode.None;
}


