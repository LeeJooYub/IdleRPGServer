using System.Threading.Tasks;
using System;
using GameAPIServer.DTO.ServiceDTO;

namespace GameAPIServer.Services.Interfaces;

public interface IAuthService
{
    public Task<LoginResult> Login(LoginCommand command);
    

}
