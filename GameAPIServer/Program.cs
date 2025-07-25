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

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.Configure<DbConfig>(configuration.GetSection(nameof(DbConfig)));
builder.Services.AddTransient<IGameDb, GameDb>();
builder.Services.AddSingleton<IMemoryDb, MemoryDb>();
builder.Services.AddSingleton<IMasterDb, MasterDb>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddControllers();

SettingLogger();

var app = builder.Build();

if (!await app.Services.GetService<IMasterDb>().Load())
{
    return;
}

// log setting
// var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
// app.UseMiddleware<GameAPIServer.Middleware.VersionCheckMiddleware>();
app.UseMiddleware<GameAPIServer.Middleware.CheckUserAuthMiddleware>();
app.UseMiddleware<GameAPIServer.Middleware.LockRedisMiddleware>();

app.UseRouting();
app.MapDefaultControllerRoute();

var masterDataDB = app.Services.GetRequiredService<IMasterDb>();
await masterDataDB.Load();

app.Run(configuration["ServerAddress"]);

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