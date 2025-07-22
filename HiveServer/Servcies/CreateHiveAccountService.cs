using HiveServer.Model.DTO;
using HiveServer.Model.Entity;
using HiveServer.Repository.Interfaces;
using HiveServer.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using ZLogger;


namespace HiveServer.Services;

public class CreateHiveAccountService : ICreateHiveAccountService
{
    private readonly IHiveDb _hiveDb;
    private readonly ILogger<CreateHiveAccountService> _logger;

    public CreateHiveAccountService(IHiveDb hiveDb, ILogger<CreateHiveAccountService> logger)
    {
        _logger = logger;
        _hiveDb = hiveDb;

    }

    public async Task<ErrorCode> CreateAccountAsync(CreateHiveAccountRequest request)
    {
        var saltValue = Security.SaltString();
        var hashedPw = Security.MakeHashingPassword(saltValue, request.Password);
        _logger.ZLogDebug($"[CreateAccount] email: {request.Email}, salt: {saltValue}, hash: {hashedPw}");
        try
        {
            _logger.ZLogDebug($"hi");
            var result = await _hiveDb.InsertAccountAsync(new AccountInfo
            {
                player_id = 0, // Auto incremented by DB
                email = request.Email,
                pw = hashedPw,
                salt = saltValue,
                create_dt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
            });
            _logger.ZLogDebug($"[CreateAccount] Inserted account with result: {result}");
            return result != 1 ? ErrorCode.CreateAccountFailInsert : ErrorCode.None;
        }
        catch (Exception ex)
        {
            _logger.ZLogDebug($"[CreateAccount] Exception: {ex}");
            return ErrorCode.CreateAccountFailException;
        }
    }
}

