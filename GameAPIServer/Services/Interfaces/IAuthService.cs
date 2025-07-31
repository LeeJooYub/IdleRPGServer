using System.Threading.Tasks;
using System;
using GameAPIServer.DTO.Service;

namespace GameAPIServer.Services.Interfaces;

public interface IAuthService
{
    public Task<LoginOutput> Login(LoginInput input);
    

}
