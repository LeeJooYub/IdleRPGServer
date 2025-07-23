
using System.Data;
using System.Threading.Tasks;

using MySqlConnector;
using SqlKata.Execution;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using GameAPIServer.Repository.Interfaces;
using GameAPIServer.Models;

namespace GameAPIServer.Repository;

public class MasterDb : IMasterDb
{
    private readonly IOptions<DbConfig> _dbConfig;
    private readonly ILogger<MasterDb> _logger;
    private IDbConnection _dbConn;
    private readonly SqlKata.Compilers.MySqlCompiler _compiler;
    private readonly QueryFactory _queryFactory;
    private readonly IMemoryDb _memoryDb;
    private readonly IGameDb _gameDb;

    public VersionDAO _version { get; set; }

    public MasterDb(
        ILogger<MasterDb> logger,
        IOptions<DbConfig> dbConfig,
        IMemoryDb memoryDb,
        IGameDb gameDb)
    {
        _logger = logger;
        _dbConfig = dbConfig;

        Open();

        _compiler = new SqlKata.Compilers.MySqlCompiler();
        _queryFactory = new QueryFactory(_dbConn, _compiler);
        _memoryDb = memoryDb;
        _gameDb = gameDb;
    }

    public async Task<bool> Load()
    {
        return true;
    }

    public void Dispose()
    {
        Close();
    }

    private void Open()
    {
        _dbConn = new MySqlConnection(_dbConfig.Value.MasterDb);
        _dbConn.Open();
    }

    private void Close()
    {
        _dbConn.Close();
    }
}
