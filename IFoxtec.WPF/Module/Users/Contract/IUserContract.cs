using Abp.Application.Services.Dto;
using IFoxtec.Users.Dto;
using IFoxtec.WPF.Common.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.WPF.Module.Users
{
    public interface IUserContract : ICrudContract<UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>
    {

    }
}
