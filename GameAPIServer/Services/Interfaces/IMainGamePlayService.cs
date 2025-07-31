using System.Threading.Tasks;
using GameAPIServer.DTO.Service;

namespace GameAPIServer.Services.Interfaces
{
    public interface IMainGamePlayService
    {
        Task<ClearStageOutput> ClearStageAsync(ClearStageInput input);
        Task<ClearGuideMissionOutput> ClearGuideMissionAsync(ClearGuideMissionInput input);
    }
}
