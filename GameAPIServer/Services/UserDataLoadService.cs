using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using GameAPIServer.Repository.Interfaces;
using GameAPIServer.Services.Interfaces;

using GameAPIServer.DTO.Controller;
using GameAPIServer.DTO.Service;
using GameAPIServer.Models.GameDB;
using GameAPIServer.Models.MasterDB;
using System.Runtime.InteropServices;

namespace GameAPIServer.Services;

public class UserDataLoadService : IUserDataLoadService
{
    private readonly ILogger<UserDataLoadService> _logger;
    private readonly IGameDb _gameDb;
    private readonly IMemoryDb _memoryDb;

    public UserDataLoadService(
        ILogger<UserDataLoadService> logger,
        IGameDb gameDb,
        IMemoryDb memoryDb)
    {
        _logger = logger;
        _gameDb = gameDb;
        _memoryDb = memoryDb;
    }
}
