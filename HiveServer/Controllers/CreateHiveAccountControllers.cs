using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using HiveServer.Model.DTO;
using HiveServer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;



namespace HiveServer.Controllers;

[ApiController]
[Route("[controller]")]
public class CreateHiveAccountController : ControllerBase
{
    readonly ILogger<CreateHiveAccountController> _logger;
    readonly IHiveDb _hiveDb;

    public CreateHiveAccountController(ILogger<CreateHiveAccountController> logger, IHiveDb hiveDb)
    {
        _logger = logger;
        _hiveDb = hiveDb;
    }

    [HttpPost]
    public async Task<CreateHiveAccountResponse> CreateHiveAccount([FromBody] CreateHiveAccountRequest request)
    {
        CreateHiveAccountResponse response = new();

        response.Result = await _hiveDb.CreateAccountAsync(request.Email, request.Password);

        return response;
    }



}
