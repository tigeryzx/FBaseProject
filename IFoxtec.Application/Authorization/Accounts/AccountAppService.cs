using System.Threading.Tasks;
using Abp.Zero.Configuration;
using IFoxtec.Authorization.Accounts.Dto;
using IFoxtec.Authorization.Users;
using Abp.Configuration;
using IFoxtec.MultiTenancy.Dto;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace IFoxtec.Authorization.Accounts
{
    public class AccountAppService : IFoxtecAppServiceBase, IAccountAppService
    {
        private readonly UserRegistrationManager _userRegistrationManager;

        public AccountAppService(
            UserRegistrationManager userRegistrationManager)
        {
            _userRegistrationManager = userRegistrationManager;
        }

        public async Task<ListResultDto<TenantDto>> GetActiveTenant()
        {
            var tenantList = await TenantManager.GetActiveTenant();
            return new ListResultDto<TenantDto>()
            {
                Items = tenantList.MapTo<List<TenantDto>>()
            };
            
        }

        public async Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input)
        {
            var tenant = await TenantManager.FindByTenancyNameAsync(input.TenancyName);
            if (tenant == null)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.NotFound);
            }

            if (!tenant.IsActive)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.InActive);
            }

            return new IsTenantAvailableOutput(TenantAvailabilityState.Available, tenant.Id);
        }

        public async Task<RegisterOutput> Register(RegisterInput input)
        {
            var user = await _userRegistrationManager.RegisterAsync(
                input.Name,
                input.Surname,
                input.EmailAddress,
                input.UserName,
                input.Password,
                false
            );

            var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);

            return new RegisterOutput
            {
                CanLogin = user.IsActive && (user.IsEmailConfirmed || !isEmailConfirmationRequiredForLogin)
            };
        }
    }
}