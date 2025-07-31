using System;
using System.Threading.Tasks;
using HiveServer.Model.DTO;

namespace HiveServer.Services.Interfaces
{
    public interface ILoginService
    {

        Task<(ErrorCode, Int64)> Login(LoginHiveRequest request);
        //public VerifyTokenResponse Verify(VerifyTokenBody request);
    }
}