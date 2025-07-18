using System;
using System.Threading.Tasks;
using HiveServer.Model.DTO;

namespace HiveServer.Servcies.Interfaces
{
    public interface ILoginService
    {
        Task<LoginHiveResponse> Login(LoginHiveRequest request);
        Task<(ErrorCode, Int64)> VerifyUser(LoginHiveRequest request);
        //public VerifyTokenResponse Verify(VerifyTokenBody request);
    }
}