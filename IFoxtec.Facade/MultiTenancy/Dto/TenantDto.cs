using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.Facade.MultiTenancy.Dto
{
    public class TenantDto : EntityDto
    {
        [Required]
        public string TenancyName { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
