using System.Threading.Tasks;

using GameAPIServer.Models;

namespace GameAPIServer.Repository.Interfaces;

public interface IMasterDb
{
    public Version _version { get; }

    
    public Task<bool> Load();
}
