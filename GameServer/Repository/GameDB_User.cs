using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using MySqlConnector;
using SqlKata.Execution;

using GameAPIServer.Repository.Interfaces;
using GameAPIServer.Models.GameDB;


namespace GameAPIServer.Repository;

public partial class GameDb : IGameDb
{
    readonly ILogger<GameDb> _logger;
    readonly IOptions<DbConfig> _dbConfig;
    
    IDbConnection _dbConn;
    SqlKata.Compilers.MySqlCompiler _compiler;
    QueryFactory _queryFactory;

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

    public async Task<GdbUserInfo> GetUserByPlayerId(Int64 playerId)
    {
        return await _queryFactory.Query("user")
                                .Where("player_id", playerId)
                                .FirstOrDefaultAsync<GdbUserInfo>();
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