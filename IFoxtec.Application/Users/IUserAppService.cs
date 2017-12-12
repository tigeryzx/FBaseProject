using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using IFoxtec.Roles.Dto;
using IFoxtec.Users.Dto;

namespace IFoxtec.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();
    }
}