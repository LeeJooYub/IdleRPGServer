using System.Threading.Tasks;
using GameAPIServer.DTO.Service;

namespace GameAPIServer.Services.Interfaces
{
    public interface IMainGamePlayService
    {
        Task<> StageClear( );
        Task<> ClearGuideMission( );
    }
}
