using AutoMapper;
using IFoxtec.MultiTenancy.Dto;
using IFoxtec.WPF.Module.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Mappers;
using IFoxtec.Users.Dto;
using IFoxtec.WPF.Module.Users;
using IFoxtec.Roles.Dto;

namespace IFoxtec.WPF.Module
{
    public class MappingConfig
    {
        public static void Register()
        {
            Mapper.Initialize(cfg=> 
            {
                cfg.CreateMap<TenantDto, TenantItemModel>();

                cfg.CreateMap<UserDto, UserModel>();
                cfg.CreateMap<CreateUserModel, CreateUserDto>();
                cfg.CreateMap<RoleDto, RoleItemModel>();
            });
        }
    }
}
