using Abp.Application.Services.Dto;
using IFoxtec.Facade.Contract;
using IFoxtec.Facade.WebApi.Contract.Common;
using IFoxtec.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.Facade.WebApi.Contract
{
    public class UserContract : BaseCrudContract<UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>, IUserContract
    {
        public UserContract():
            base("http://localhost:6634/api/services/app/user")
        {

        }
    }
}
