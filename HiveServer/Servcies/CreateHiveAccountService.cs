using HiveServer.Model.DTO;
using HiveServer.Model.Entity;
using HiveServer.Repository.Interfaces;
using HiveServer.Servcies.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using ZLogger;


namespace HiveServer.Servcies;

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
        try
        {
            var saltValue = Security.SaltString();
            var hashedPw = Security.MakeHashingPassword(saltValue, request.Password);

            var result = await _hiveDb.InsertAccountAsync(new AccountInfo
            {
                player_id = 0,
                email = request.Email,
                pw = hashedPw,
                create_dt = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
            });

            _logger.ZLogDebug($"[CreateAccount] email: {request.Email}, salt: {saltValue}, hash: {hashedPw}");

            return result != 1 ? ErrorCode.CreateAccountFailInsert : ErrorCode.None;
        }
        catch (Exception ex)
        {
            _logger.ZLogError($"[CreateAccount] Exception: {ex}");
            return ErrorCode.CreateAccountFailException;
        }
    }
}

