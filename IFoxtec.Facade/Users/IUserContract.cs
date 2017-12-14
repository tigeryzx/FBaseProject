using Abp.Application.Services.Dto;
using IFoxtec.Facade.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.Facade.Contract
{
    public interface IUserContract : ICrudContract<UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>
    {

    }
}
