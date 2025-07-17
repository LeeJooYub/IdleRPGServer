using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HiveServer.Model.DTO;
using HiveServer.Repository;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using HiveServer.Security;

namespace HiveServer.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginHive : ControllerBase
{
    readonly string _saltValue;
    readonly ILogger<LoginHive> _logger;
    readonly IHiveDb _hiveDb;
    
    public LoginHive(ILogger<LoginHive> logger, IHiveDb hiveDb, IConfiguration config)
    {
        _saltValue = config.GetSection("TokenSaltValue").Value;
        _logger = logger;
        _hiveDb = hiveDb;
    }

    [HttpPost]
    public async Task<LoginHiveResponse> Login([FromBody] LoginHiveRequest request)
    {
        LoginHiveResponse response = new();
        (response.Result, response.PlayerId) = await _hiveDb.VerifyUser(request.Email, request.Password);
        response.HiveToken = EncryptionService.MakeHashingToken(_saltValue, response.PlayerId);

        return response;

    }
}
