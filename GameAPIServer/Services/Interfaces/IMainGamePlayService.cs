using System.Threading.Tasks;
using GameAPIServer.DTO.ServiceDTO;

namespace GameAPIServer.Services.Interfaces
{
    public interface IMainGamePlayService
    {
        Task<StageClearOutput> StageClear(StageClearInput input);
        Task<ErrorCode> ClearGuideMission(ClearGuideMissionInput input);
    }
}
