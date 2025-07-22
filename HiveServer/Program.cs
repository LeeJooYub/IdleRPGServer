
using System;
using System.IO;

using HiveServer.Repository;
using HiveServer.Repository.Interfaces;
using HiveServer.Services;
using HiveServer.Services.Interfaces;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using ZLogger;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

builder.Services.Configure<DbConfig>(configuration.GetSection(nameof(DbConfig)));
builder.Services.AddTransient<IHiveDb, HiveDb>();
builder.Services.AddTransient<ICreateHiveAccountService, CreateHiveAccountService>();
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddControllers();

SettingsLoggers();

WebApplication app = builder.Build();


ILoggerFactory loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
//LogManager.SetLoggerFactory(loggerFactory, "Global");

app.MapControllers();
app.Run(configuration["ServerAddress"]);

void SettingsLoggers()
{
    ILoggingBuilder logging = builder.Logging;
    logging.ClearProviders();

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
    });
}