using Abp.Application.Services;
using Abp.Application.Services.Dto;
using IFoxtec.MultiTenancy.Dto;

namespace IFoxtec.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
