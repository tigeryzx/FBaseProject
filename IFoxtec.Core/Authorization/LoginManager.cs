using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Zero.Configuration;
using IFoxtec.Authorization.Roles;
using IFoxtec.Authorization.Users;
using IFoxtec.MultiTenancy;
using System.Linq;
using System.Threading.Tasks;

namespace IFoxtec.Authorization
{
    public class LogInManager : AbpLogInManager<Tenant, Role, User>
    {
        public LogInManager(
            UserManager userManager,
            IMultiTenancyConfig multiTenancyConfig,
            IRepository<Tenant> tenantRepository,
            IUnitOfWorkManager unitOfWorkManager,
            ISettingManager settingManager,
            IRepository<UserLoginAttempt, long> userLoginAttemptRepository,
            IUserManagementConfig userManagementConfig, IIocResolver iocResolver,
            RoleManager roleManager)
            : base(
                  userManager,
                  multiTenancyConfig,
                  tenantRepository,
                  unitOfWorkManager,
                  settingManager,
                  userLoginAttemptRepository,
                  userManagementConfig,
                  iocResolver,
                  roleManager)
        {
        }

        public async Task<ListResultDto<Tenant>> GetActiveTenant()
        {
            var tenantList = await Task.Run(() =>
            {
                return base.TenantRepository
                .GetAll()
                .Where(x => x.IsActive)
                .ToList();
            });

            return new ListResultDto<Tenant>()
            {
                Items = tenantList
            };

        }
    }
}
