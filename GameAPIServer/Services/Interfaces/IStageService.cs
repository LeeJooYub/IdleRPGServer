using System.Threading.Tasks;
using GameAPIServer.DTO.Service;

namespace GameAPIServer.Services.Interfaces
{
    public interface IStageService
    {
        Task<ClearStageOutput> ClearStageAsync(ClearStageInput input);
    }
}
