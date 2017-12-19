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
    public class UserContract : BaseCrudContract<UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>, IUserContract
    {
        public UserContract():
            base("http://localhost:6634/api/services/app/user")
        {

        }
    }
}
