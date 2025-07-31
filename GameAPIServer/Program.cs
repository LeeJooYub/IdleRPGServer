using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ZLogger;

using GameAPIServer.Repository;
using GameAPIServer.Repository.Interfaces;
using GameAPIServer.Services;
using GameAPIServer.Services.Interfaces;

using MySqlConnector;
using System;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// 1. 데이터베이스 연결 테스트
TestDbConnection();

// 2. 서비스 등록
builder.Services.Configure<DbConfig>(configuration.GetSection(nameof(DbConfig)));
builder.Services.AddTransient<IGameDb, GameDb>();
builder.Services.AddSingleton<IMemoryDb, MemoryDb>();
builder.Services.AddSingleton<IMasterDb, MasterDb>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IUserDataLoadService, UserDataLoadService>();
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddTransient<IAttendanceService, AttendanceService>();
builder.Services.AddTransient<IStageService, StageService>();
builder.Services.AddTransient<IMissionService, MissionService>();
builder.Services.AddControllers();

// 3. 로깅 설정
SettingLogger();

var app = builder.Build();

// 4. 마스터 데이터 로드
var masterDataDB = app.Services.GetRequiredService<IMasterDb>();
if (!await masterDataDB.Load())
{
    return;
}

// 5. 미들웨어 등록
app.UseMiddleware<GameAPIServer.Middleware.VersionCheckMiddleware>();
app.UseMiddleware<GameAPIServer.Middleware.CheckUserAuthMiddleware>();
app.UseMiddleware<GameAPIServer.Middleware.LockRedisMiddleware>();

// 6. 라우팅 및 실행
app.UseRouting();
app.MapDefaultControllerRoute();
app.Run(configuration["ServerAddress"]);





//// 아래는 함수 ///





void TestDbConnection()
{
    TestSingleDbConnection("MasterDb");
    TestSingleDbConnection("GameDb");
}

void TestSingleDbConnection(string dbName)
{
    try
    {
        var connStr = configuration.GetSection(nameof(DbConfig))[dbName];
        using var conn = new MySqlConnection(connStr);
        conn.Open();
        Console.WriteLine($"{dbName} 연결 성공!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{dbName} 연결 실패: {ex.Message}");
    }
}

void SettingLogger()
{
    var logging = builder.Logging;
    logging.ClearProviders();

    logging.AddFilter("Microsoft", LogLevel.None);
    logging.AddFilter("System", LogLevel.None);

    var fileDir = configuration["logdir"];
    if (!Directory.Exists(fileDir))
    {
        Directory.CreateDirectory(fileDir);
    }

    logging.AddZLoggerRollingFile(options =>
    {
        options.UseJsonFormatter();
        options.FilePathSelector = (timestamp, sequenceNumber) => $"{fileDir}{timestamp.ToLocalTime():yyyy-MM-dd}_{sequenceNumber:000}.log";
        options.RollingInterval = ZLogger.Providers.RollingInterval.Day;
        options.RollingSizeKB = 1024;
    });

    _ = logging.AddZLoggerConsole(options =>
    {
        options.UseJsonFormatter();
        options.ConfigureEnableAnsiEscapeCode = true; // ANSI 색상 활성화
    });
}