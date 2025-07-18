using System.Threading.Tasks;
using HiveServer.Model.DTO;

namespace HiveServer.Servcies.Interfaces
{
    public interface ICreateHiveAccountService
    {
        Task<ErrorCode> CreateAccountAsync(CreateHiveAccountRequest request);


    }
}