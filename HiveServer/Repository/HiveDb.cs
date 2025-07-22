using System;
using System.Data;
using System.Threading.Tasks;

using HiveServer.Model.Entity;
using HiveServer.Repository.Interfaces;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using MySqlConnector;
using SqlKata.Execution;

using ZLogger;

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
        _logger.ZLogDebug($"[InsertAccountAsync] called with: email={accountInfo.email}, pw={accountInfo.pw}, salt={accountInfo.salt}, create_dt={accountInfo.create_dt}");
        try
        {
            var id = await _queryFactory.Query("account_info").InsertAsync(accountInfo);
            _logger.ZLogDebug($"[InsertAccountAsync] Inserted id: {id}");
            return id;
        }
        catch (Exception ex)
        {
            _logger.ZLogError($"[InsertAccountAsync] Exception: {ex}");
            throw;
        }
    }

    public async Task<AccountInfo?> GetAccountByEmailAsync(string email)
    {
        return await _queryFactory.Query("account_info")
            .Where("email", email)
            .FirstOrDefaultAsync<AccountInfo>();
    }













    // public async Task<int> InsertCategoryAsync(TestCategory category)
    // {
    //     var data = new
    //     {
    //         slug = category.Slug,
    //         title = category.Title,
    //         description = category.Description,
    //         image = category.Image,
    //         sortid = category.SortIds,
    //         display = category.Display,
    //         created_at = category.CreatedAt,
    //         updated_at = category.UpdatedAt,
    //         deleted_at = category.DeletedAt
    //     };
    //     return await _queryFactory.Query("category").InsertAsync(data);
    // }

    // public async Task<TestCategory> GetCategoryByIdNoAliasAsync(int id)
    // {
    //     // alias 없이 DB 컬럼명(id)과 C# 프로퍼티명(Id)이 다를 때 매핑이 되는지 테스트
    //     return await _queryFactory.Query("category")
    //         .Where("id", id)
    //         .FirstOrDefaultAsync<Category>();
    // }

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