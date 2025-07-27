using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using GameAPIServer.DTO.ServiceDTO;
using GameAPIServer.DTO.ControllerDTO;
using GameAPIServer.Models.MasterDB;



namespace GameAPIServer.Services.Interfaces;

public interface IUserDataLoadService
{
    Task<GetUserCurrencyResult> GetUserCurrencyAsync(GetUserCurrencyCommand command);
    Task<GetUserInventoryResult> GetUserInventoryAsync(GetUserInventoryCommand command);
    
    //Task<GetUserCharacterInfoResult> GetUserCharacterInfoAsync(GetUserCharacterInfoCommand command);
}
