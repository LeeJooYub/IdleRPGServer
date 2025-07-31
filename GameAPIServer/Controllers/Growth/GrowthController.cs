using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using GameAPIServer.Services.Interfaces;
using GameAPIServer.Repository.Interfaces;

using ZLogger;
using GameAPIServer.DTO.Controller;
using GameAPIServer.DTO.Service;
using GameAPIServer.Models;

namespace GameAPIServer.Controllers.Attendance;

[ApiController]
[Route("growth")]
public class GrowthController : ControllerBase
{
    private readonly ILogger<GrowthController> _logger;
    private readonly IGrowthService _growthService;
    private readonly IMemoryDb _memoryDb;

    public GrowthController(
        ILogger<GrowthController> logger,
        IGrowthService growthService,
        IMemoryDb memoryDb)
    {
        _logger = logger;
        _growthService = growthService;
        _memoryDb = memoryDb;
    }


    [HttpPost("upgrade-ability")]
    public async Task<CheckTodayResponse> UpgradeAbility([FromBody] CheckTodayRequest request)
    {
        var userInfo = HttpContext.Items["userinfo"] as RdbAuthUserData;
        var checkTodayInput = new CheckTodayInput
        {
            PlayerUid = userInfo.PlayerUid,
            AttendanceBookId = request.AttendanceBookId,
            CheckNthDay = request.CheckNthDay
        };
    }



}
