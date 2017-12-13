using Abp.Web.Models;
using Abp.WebApi.Controllers;
using IFoxtec.MultiTenancy;
using System.Threading.Tasks;
using System.Web.Http;

namespace IFoxtec.Api.Controllers
{
    public class TenantController : AbpApiController
    {
        private readonly ITenantAppService _tenantAppService;

        public TenantController(ITenantAppService tenantAppService)
        {
            this._tenantAppService = tenantAppService;
        }

        [HttpPost]
        public async Task<AjaxResponse> GetActiveTenant()
        {
            var tenantList = await this._tenantAppService.GetActiveTenant();
            return new AjaxResponse(tenantList);
        }

    }
}
