using System.Data;
using System.Threading.Tasks;

using HiveServer.Model.Entity;
using HiveServer.Repository.Interfaces;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using MySqlConnector;
using SqlKata.Execution;

namespace HiveServer.Repository;


public class HiveDb : IHiveDb
{
    private readonly IOptions<DbConfig> _dbConfig;
    private readonly ILogger<HiveDb> _logger;
    private IDbConnection _dbConn;
    private readonly SqlKata.Compilers.MySqlCompiler _compiler;
    private readonly QueryFactory _queryFactory;

    public HiveDb(
        ILogger<HiveDb> logger,
        IOptions<DbConfig> dbConfig)
    {
        _logger = logger;
        _dbConfig = dbConfig;

        Open();

        _compiler = new SqlKata.Compilers.MySqlCompiler();
        _queryFactory = new QueryFactory(_dbConn, _compiler);
    }

    public void Dispose()
    {
        Close();
    }

    public async Task<int> InsertAccountAsync(AccountInfo accountInfo)
    {
        var data = new {
            player_id = accountInfo.PlayerId,
            email = accountInfo.Email,
            pw = accountInfo.Pw,
            salt_value = accountInfo.SaltValue,
            create_dt = accountInfo.CreateDt
        };
        return await _queryFactory.Query("account_info").InsertAsync(data);
    }

    public async Task<AccountInfo?> GetAccountByEmailAsync(string email)
    {
        return await _queryFactory.Query("account_info")
            .Select("player_id as PlayerId", "email as Email", "pw as Pw", "salt_value as SaltValue", "create_dt as CreateDt")
            .Where("Email", email)
            .FirstOrDefaultAsync<AccountInfo>();
    }

    private void Open()
    {
        _dbConn = new MySqlConnection(_dbConfig.Value.HiveDb);
        _dbConn.Open();
    }

    private void Close()
    {
        _dbConn.Close();
    }
}





public class DbConfig
{
    public string HiveDb { get; set; }
}