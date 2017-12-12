using Abp.AutoMapper;
using IFoxtec.Sessions.Dto;

namespace IFoxtec.Web.Models.Account
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}