using GameAPIServer.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameAPIServer.Middleware;


public class VersionCheckMiddleware
{
    readonly RequestDelegate _next;
    readonly ILogger<VersionCheckMiddleware> _logger;
    readonly IMasterDb _masterDb;

    public VersionCheckMiddleware(RequestDelegate next, ILogger<VersionCheckMiddleware> logger, IMasterDb masterDb)
    {
        _next = next;
        _logger = logger;
        _masterDb = masterDb;
    }

    public async Task Invoke(HttpContext context)
    {
        var appVersion = context.Request.Headers["AppVersion"].ToString();
        var masterDataVersion = context.Request.Headers["MasterDataVersion"].ToString();

        if (!(await VersionCompare(appVersion, masterDataVersion, context)))
        {
            return;
        }

        await _next(context);
    }

    async Task<bool> VersionCompare(string appVersion, string masterDataVersion, HttpContext context)
    {
        if (!appVersion.Equals(_masterDb._version!.app_version))
        {
            context.Response.StatusCode = StatusCodes.Status426UpgradeRequired;
            var errorJsonResponse = JsonSerializer.Serialize(new MiddlewareResponse
            {
                result = ErrorCode.InValidAppVersion
            });
            await context.Response.WriteAsync(errorJsonResponse);
            return false;
        }

        if (!masterDataVersion.Equals(_masterDb._version!.master_data_version))
        {
            context.Response.StatusCode = StatusCodes.Status426UpgradeRequired;
            var errorJsonResponse = JsonSerializer.Serialize(new MiddlewareResponse
            {
                result = ErrorCode.InvalidMasterDataVersion
            });
            await context.Response.WriteAsync(errorJsonResponse);
            return false;
        }

        return true;
    }

    class MiddlewareResponse
    {
        public ErrorCode result { get; set; }
    }
}

