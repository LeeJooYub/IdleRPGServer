using HiveServer.Model.Entity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SqlKata.Execution;
using System.Data;
using System.Threading.Tasks;
using HiveServer.Repository.Interfaces;
using MySqlConnector;


namespace HiveServer.Repository;

public class HiveDb : IHiveDb
{
    readonly IOptions<DbConfig> _dbConfig;
    readonly ILogger<HiveDb> _logger;
    IDbConnection _dbConn;
    readonly SqlKata.Compilers.MySqlCompiler _compiler;
    readonly QueryFactory _queryFactory;

    public HiveDb(ILogger<HiveDb> logger, IOptions<DbConfig> dbConfig)
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
        return await _queryFactory.Query("account_info").InsertAsync(accountInfo);
    }


    public async Task<AccountInfo?> GetAccountByEmailAsync(string email)
    {
        return await _queryFactory.Query("account_info")
        .Where("Email", email)
        .FirstOrDefaultAsync<AccountInfo>();
    }

    void Open()
    {
        _dbConn = new MySqlConnection(_dbConfig.Value.HiveDb);
        _dbConn.Open();
    }
    void Close()
    {
        _dbConn.Close();
    }
}





public class DbConfig
{
    public string HiveDb { get; set; }
}