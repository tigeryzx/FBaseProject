using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using IFoxtec.Authorization.Accounts.Dto;
using IFoxtec.MultiTenancy.Dto;

namespace IFoxtec.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);

        Task<ListResultDto<TenantDto>> GetActiveTenant();
    }
}
