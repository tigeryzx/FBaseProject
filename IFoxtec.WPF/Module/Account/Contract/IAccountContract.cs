using Abp.Application.Services.Dto;
using IFoxtec.MultiTenancy.Dto;
using IFoxtec.WPF.Common.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.WPF.Module.Account
{
    public interface IAccountContract : IContract
    {
        Task<LoginResultDto> Login(LoginDto input);

        Task<ListResultDto<TenantDto>> GetActiveTenant();
    }
}
