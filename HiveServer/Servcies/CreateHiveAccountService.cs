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

    public async Task<ErrorCode> CreateAccountAsync(CreateHiveAccountInput input)
    {
        var saltValue = Security.SaltString();
        var hashedPw = Security.MakeHashingPassword(saltValue, input.Password);

        try
        {
            var result = await _hiveDb.InsertAccountAsync(new AccountInfo
            {
                email = input.Email,
                pwd = hashedPw,
                salt = saltValue
            });
            _logger.ZLogDebug($"[CreateAccount] Inserted account with result: {result}");


            if (result == 0)
            {
                _logger.ZLogDebug($"[CreateAccount] Inserted account returned 0, indicating failure.");
                return ErrorCode.CreateAccountFailInsert;
            }

            return ErrorCode.None;
        }

        catch (Exception ex)
        {
            _logger.ZLogDebug($"[CreateAccount] Exception: {ex}");
            return ErrorCode.CreateAccountFailException;
        }
    }
}

