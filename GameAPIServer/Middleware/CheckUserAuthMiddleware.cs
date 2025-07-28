using GameAPIServer.Models;
using GameAPIServer.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameAPIServer.Middleware;

public class CheckUserAuthMiddleware
{
    readonly RequestDelegate _next;

    public CheckUserAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        //로그인, 회원가입 api는 토큰 검사를 하지 않는다.
        var formString = context.Request.Path.Value;
        if (string.Compare(formString, "/Login", StringComparison.OrdinalIgnoreCase) == 0)
        {
            // Call the next delegate/middleware in the pipeline
            await _next(context);

            return;
        }

        // token이 있는지 검사하고 있다면 저장
        var (isTokenNotExist, token) = await IsTokenNotExistOrReturnToken(context);
        if (isTokenNotExist)
        {
            return;
        }

        //uid가 있는지 검사하고 있다면 저장
        var (isUidNotExist, uid) = await IsUidNotExistOrReturnUid(context);
        if (isUidNotExist)
        {
            return;
        }

        context.Items["account_id"] = uid;
        context.Items["game_server_token"] = token;

        await _next(context);

    }



    async Task<(bool, string)> IsTokenNotExistOrReturnToken(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue("token", out var token))
        {
            return (false, token);
        }

        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        var errorJsonResponse = JsonSerializer.Serialize(new MiddlewareResponse
        {
            result = ErrorCode.TokenDoesNotExist
        });
        await context.Response.WriteAsync(errorJsonResponse);

        return (true, "");
    }

    async Task<(bool, string)> IsUidNotExistOrReturnUid(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue("uid", out var uid))
        {
            return (false, uid);
        }

        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        var errorJsonResponse = JsonSerializer.Serialize(new MiddlewareResponse
        {
            result = ErrorCode.UidDoesNotExist
        });
        await context.Response.WriteAsync(errorJsonResponse);

        return (true, "");
    }

    class MiddlewareResponse
    {
        public ErrorCode result { get; set; }
    }
}
