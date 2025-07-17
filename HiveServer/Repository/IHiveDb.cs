using System;
using System.Threading.Tasks;

namespace HiveServer.Repository
{
    public interface IHiveDb : IDisposable
    {
        public Task<ErrorCode> CreateAccountAsync(string email, string pw);
        public Task<(ErrorCode, Int64)> VerifyUser(string email, string pw);
    }
}
