using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using HiveServer.Model.DTO;
using HiveServer.Repository.Interfaces;
using HiveServer.Servcies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HiveServer.Servcies.Interfaces;


namespace HiveServer.Controllers;

[ApiController]
[Route("[controller]")]
public class CreateHiveAccountController : ControllerBase
{
    readonly ILogger<CreateHiveAccountController> _logger;
    readonly IHiveDb _hiveDb;
    readonly ICreateHiveAccountService _createHiveAccountService;

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

        response.Result = await _createHiveAccountService.CreateAccountAsync(request);

        return response;
    }



}
