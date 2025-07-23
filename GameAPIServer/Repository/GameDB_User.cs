using System;
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

    public async Task<(ErrorCode, Int64)> FindUserByPlatformId(Int64 platformId)
    {
        AccountInfo? result = await _queryFactory.Query("user")
            .Where("platform_id", platformId)
            .FirstOrDefaultAsync<AccountInfo>(); // ★ 제네릭 타입 명시!

        if (result == null)
        {
            return (ErrorCode.LoginFailUserNotExist, 0);
        }

        return (ErrorCode.None, result.account_id);
    }

    public async Task<(ErrorCode, Int64)> CreateUser(AccountInfo userInfo)
    {
        Int64 result = await _queryFactory.Query("user").InsertAsync(userInfo);
        if (result == 0)
        {
            return (ErrorCode.CreateUserFailException, 0);
        }

        return (ErrorCode.None, result);
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