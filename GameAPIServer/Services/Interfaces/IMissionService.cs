using System.Threading.Tasks;
using GameAPIServer.DTO.Service;

namespace GameAPIServer.Services.Interfaces
{
    public interface IMissionService
    {
        Task<ClearGuideMissionOutput> ClearGuideMissionAsync(ClearGuideMissionInput input);
    }
}
