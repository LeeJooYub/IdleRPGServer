using System;
using System.Text.Json;
using System.Data;
using System.Threading.Tasks;

using MySqlConnector;
using SqlKata.Execution;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using GameAPIServer.Repository.Interfaces;
using GameAPIServer.Models.MasterDB;
using GameAPIServer.Models;

namespace GameAPIServer.Repository;

public partial class MasterDb : IMasterDb
{
    private readonly IOptions<DbConfig> _dbConfig;
    private readonly ILogger<MasterDb> _logger;
    private IDbConnection _dbConn;
    private readonly SqlKata.Compilers.MySqlCompiler _compiler;
    private readonly QueryFactory _queryFactory;
    private readonly IMemoryDb _memoryDb;
    private readonly IGameDb _gameDb;

    public GameAPIServer.Models.MasterDB.Version _version { get; set; }

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
        try
        {
            var versionRow = await _queryFactory.Query("version").FirstOrDefaultAsync<GameAPIServer.Models.MasterDB.Version>();
            if (versionRow == null)
            {
                _logger.LogError("version 테이블에 데이터가 없습니다.");
                _version = null;
                return false;
            }
            _version = versionRow;
            _logger.LogInformation("version 정보 로드 성공: AppVersion={AppVersion}, MasterDataVersion={MasterDataVersion}", _version.app_version, _version.master_data_version);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "version 정보 로드 실패");
            _version = null;
            return false;
        }
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
