using GameAPIServer.Models;
using GameAPIServer.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

using GameAPIServer.Repository; 

namespace GameAPIServer.Middleware;

public class LockRedisMiddleware
{
    readonly IMemoryDb _memoryDb;
    readonly RequestDelegate _next;

    public LockRedisMiddleware(RequestDelegate next, IMemoryDb memoryDb)
    {
        _memoryDb = memoryDb;
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        //로그인 api는 토큰 검사를 하지 않는다.
        var formString = context.Request.Path.Value;
        if (string.Compare(formString, "/Login", StringComparison.OrdinalIgnoreCase) == 0 ||
            string.Compare(formString, "/Logout", StringComparison.OrdinalIgnoreCase) == 0)
        {
            // Call the next delegate/middleware in the pipeline
            await _next(context);

            return;
        }

        var userInfo = context.Items["userinfo"] as RdbAuthUserData;


        //이번 api 호출 끝날 때까지 redis키 잠금 만약 이미 잠겨있다면 에러
        var userLockKey = MemoryDbKeyMaker.UserLockKey(userInfo.TokenKey.ToString());
        if (await SetLockAndIsFailThenSendError(context, userLockKey))
        {
            return;
        }

        // Call the next delegate/middleware in the pipeline
        await _next(context);

        // 트랜잭션 해제(Redis 동기화 해제)
        await _memoryDb.DelUserReqLockAsync(userLockKey);
    }


    async Task<bool> SetLockAndIsFailThenSendError(HttpContext context, string AuthToken)
    {
        if (await _memoryDb.SetUserReqLockAsync(AuthToken))
        {
            return false;
        }

        context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        var errorJsonResponse = JsonSerializer.Serialize(new MiddlewareResponse
        {
            result = ErrorCode.AuthTokenFailSetNx
        });
        await context.Response.WriteAsync(errorJsonResponse);
        return true;
    }
    


    class MiddlewareResponse
    {
        public ErrorCode result { get; set; }
    }

}
