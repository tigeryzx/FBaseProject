using Abp.Application.Services.Dto;
using IFoxtec.Common.WPF.Config;
using IFoxtec.Facade.Account;
using IFoxtec.Facade.Account.Dto;
using IFoxtec.Facade.WebApi.Contract.Common;
using IFoxtec.MultiTenancy.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.Facade.WebApi.Contract
{
    public class AccountContract : BaseContract, IAccountContract
    {
        ApiHelper _api;
        IConfigManager _configManager;

        public AccountContract(IConfigManager configManager)
        {
            this._api = new ApiHelper();
            this._configManager = configManager;
        }

        private const string Url_LoginUrl = "http://localhost:6634/api/Account/Authenticate";
        private const string Url_GetActiveTenantUrl = "http://localhost:6634/api/Account/Authenticate";



        public Task<LoginResultDto> Login(LoginDto input)
        {
            return Task.Run(()=> {
                var result = this._api.SrcPost<string>(Url_LoginUrl, input);
                var loginResult = new LoginResultDto()
                {
                    Success = result.Success,
                };

                if(!result.Success && result.Error != null)
                {
                    loginResult.ErrorMessage = result.Error.Message;
                    loginResult.ErrorDetails = result.Error.Details;
                }
                else
                {
                    this._configManager.Set(ConfigIndex.Token, result.Result);
                }
                return loginResult;
            });
        }

        public Task<ListResultDto<TenantDto>> GetActiveTenant()
        {
            return Task.Run(() => {
                return this._api.Post<ListResultDto<TenantDto>>(Url_GetActiveTenantUrl);
            });
        }
    }
}
