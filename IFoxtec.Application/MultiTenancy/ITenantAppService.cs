using Abp.Application.Services;
using Abp.Application.Services.Dto;
using IFoxtec.MultiTenancy.Dto;
using System.Threading.Tasks;

namespace IFoxtec.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
        Task<ListResultDto<TenantDto>> GetActiveTenant();

    }
}
