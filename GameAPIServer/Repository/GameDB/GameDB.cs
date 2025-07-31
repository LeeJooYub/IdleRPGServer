using System;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using MySqlConnector;
using SqlKata.Execution;
using SqlKata.Extensions;

using GameAPIServer.Repository.Interfaces;
using GameAPIServer.Models.GameDB;
using GameAPIServer.Models.MasterDB;
using GameAPIServer.DTO.Service;
using GameAPIServer.DTO.Controller;
using System.Linq;
using System.Runtime.CompilerServices;


namespace GameAPIServer.Repository;


public partial class GameDb : IGameDb
{
    private readonly ILogger<GameDb> _logger;
    private readonly IOptions<DbConfig> _dbConfig;
    
    private IDbConnection _dbConn;
    private SqlKata.Compilers.MySqlCompiler _compiler;
    private QueryFactory _queryFactory;

    public GameDb(ILogger<GameDb> logger, IOptions<DbConfig> dbConfig)
    {
        _dbConfig = dbConfig;
        _logger = logger;

        Open();

        _compiler = new SqlKata.Compilers.MySqlCompiler();
        _queryFactory = new SqlKata.Execution.QueryFactory(_dbConn, _compiler);
    }

    public void Dispose()
    {
        Close();
    }


    public IDbConnection GDbConnection()
    {
        return _queryFactory.Connection;
    }

    void Open()
    {
        _dbConn = new MySqlConnection(_dbConfig.Value.GameDb);
        _dbConn.Open();
    }

    void Close()
    {
        _dbConn.Close();
    }
}


public class DbConfig
{
    public string MasterDb { get; set; }
    public string GameDb { get; set; }
    public string Redis { get; set; }
}