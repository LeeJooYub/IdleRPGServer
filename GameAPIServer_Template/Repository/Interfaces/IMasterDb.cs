using System.Collections.Generic;
using System.Threading.Tasks;
using GameAPIServer.Models;

namespace GameAPIServer.Repository.Interfaces;

public interface IMasterDb
{
    public VersionDAO _version { get; }

    
    public Task<bool> Load();
}
