using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HiveServer.Model.DTO;
using HiveServer.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using HiveServer.Servcies;
using HiveServer.Servcies.Interfaces;
namespace HiveServer.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginHiveController : ControllerBase
{
    readonly string _saltValue;
    readonly ILogger<LoginHiveController> _logger;
    readonly IHiveDb _hiveDb;
    readonly ILoginService _loginService;

    public LoginHiveController(ILogger<LoginHiveController> logger, IHiveDb hiveDb,
    IConfiguration config, ILoginService loginService)
    {
        _saltValue = config.GetSection("TokenSaltValue").Value;
        _logger = logger;
        _hiveDb = hiveDb;
        _loginService = loginService;
    }

    [HttpPost]
    public async Task<LoginHiveResponse> Login([FromBody] LoginHiveRequest request)
    {
        LoginHiveResponse response = new();
        (response.Result, response.PlayerId) = await _loginService.VerifyUser(request);
        response.HiveToken = Security.MakeHashingToken(_saltValue, response.PlayerId);

        return response;

    }
}
