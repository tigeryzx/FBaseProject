using AutoMapper;
using IFoxtec.MultiTenancy.Dto;
using IFoxtec.WPF.Module.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Mappers;

namespace IFoxtec.WPF.Module
{
    public class MappingConfig
    {
        public static void Register()
        {
            Mapper.Initialize(cfg=> 
            {
                cfg.CreateMap<TenantDto, TenantItemModel>();
            });
        }
    }
}
