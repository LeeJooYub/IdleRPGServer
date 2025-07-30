using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using GameAPIServer.Services.Interfaces;
using GameAPIServer.Repository.Interfaces;

using ZLogger;
using GameAPIServer.DTO.Controller.DTO;
using GameAPIServer.Models;

namespace GameAPIServer.Controllers.Attendance;

[ApiController]
[Route("attendance")]
public class AttendanceController : ControllerBase
{
    private readonly ILogger<AttendanceController> _logger;
    private readonly IAttendanceService _attendanceService;
    private readonly IMemoryDb _memoryDb;

    public AttendanceController(
        ILogger<AttendanceController> logger,
        IAttendanceService attendanceService,
        IMemoryDb memoryDb)
    {
        _logger = logger;
        _attendanceService = attendanceService;
        _memoryDb = memoryDb;
    }


    [HttpPost("check-today")]
    public async Task<CheckTodayResponse> CheckTodayAttendance([FromBody] CheckTodayRequest request)
    {    
        var userInfo = HttpContext.Items["userinfo"] as RdbAuthUserData;
        var utcNow = DateTime.UtcNow;
        var (errorCode, rewardData) = await _attendanceService.CheckTodayAsync(userInfo.AccountUid, request.AttendanceBookId, utcNow);
        var response = new CheckTodayResponse
        {
            ErrorCode = errorCode,
            Reward = rewardData
        };

        return response;
    }



}


