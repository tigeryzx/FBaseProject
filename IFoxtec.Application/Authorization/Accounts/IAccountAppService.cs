using System.Threading.Tasks;
using Abp.Application.Services;
using IFoxtec.Authorization.Accounts.Dto;

namespace IFoxtec.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
