using Abp.Application.Services.Dto;
using IFoxtec.Facade.Account.Dto;
using IFoxtec.Facade.Contract;
using IFoxtec.MultiTenancy.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.Facade.Account
{
    public interface IAccountContract : IContract
    {
        Task<LoginResultDto> Login(LoginDto input);

        Task<ListResultDto<TenantDto>> GetActiveTenant();
    }
}
