using Abp.Authorization;
using IFoxtec.Authorization.Roles;
using IFoxtec.Authorization.Users;

namespace IFoxtec.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
