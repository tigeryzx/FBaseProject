using Abp.AutoMapper;
using IFoxtec.MultiTenancy.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.WPF.Module.Account
{
    [AutoMapFrom(typeof(TenantDto))]
    public class TenantItemModel
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
