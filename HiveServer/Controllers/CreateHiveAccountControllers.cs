using System.Threading.Tasks;

using HiveServer.Model.DTO;
using HiveServer.Services.Interfaces;
using HiveServer.Repository.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using ZLogger;

namespace HiveServer.Controllers;

[ApiController]
[Route("[controller]")]
public class CreateHiveAccountController : ControllerBase
{
    private readonly ILogger<CreateHiveAccountController> _logger;
    private readonly IHiveDb _hiveDb;
    private readonly ICreateHiveAccountService _createHiveAccountService;

    public CreateHiveAccountController(ILogger<CreateHiveAccountController> logger, IHiveDb hiveDb, ICreateHiveAccountService createHiveAccountService)
    {
        _createHiveAccountService = createHiveAccountService;
        _logger = logger;
        _hiveDb = hiveDb;
    }

    [HttpPost("")]
    public async Task<CreateHiveAccountResponse> CreateHiveAccount([FromBody] CreateHiveAccountRequest request)
    {
        CreateHiveAccountResponse response = new();
        _logger.ZLogDebug($"[CreateHiveAccount] called with Email: {request.Email}, Password: {request.Password}");
        response.Result = await _createHiveAccountService.CreateAccountAsync(request);

        return response;
    }



}
