using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using IFoxtec.Authorization.Users;

namespace IFoxtec.Authorization.Roles
{
    public class RoleStore : AbpRoleStore<Role, User>
    {
        public RoleStore(
            IRepository<Role> roleRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<RolePermissionSetting, long> rolePermissionSettingRepository)
            : base(
                roleRepository,
                userRoleRepository,
                rolePermissionSettingRepository)
        {
        }
    }
}