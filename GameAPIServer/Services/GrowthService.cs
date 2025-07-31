using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameAPIServer.Models.GameDB;
using GameAPIServer.Models.MasterDB;
using GameAPIServer.Models;
using GameAPIServer.DTO.Service;


using GameAPIServer.Repository.Interfaces;
using GameAPIServer.Services.Interfaces;

namespace GameAPIServer.Services;

public class GrowthService : IGrowthService
{

    private readonly IGameDb _gameDb;
    private readonly IMasterDb _masterDb;

    public GrowthService(IGameDb gameDb, IMasterDb masterDb)
    {
        _gameDb = gameDb;
        _masterDb = masterDb;
    }

}

