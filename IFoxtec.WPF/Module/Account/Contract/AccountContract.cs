using Abp.Application.Services.Dto;
using Abp.Web.Models;
using IFoxtec.Facade.WebApi;
using IFoxtec.MultiTenancy.Dto;
using IFoxtec.WPF.Common.Config;
using IFoxtec.WPF.Common.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.WPF.Module.Account
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
        private const string Url_GetActiveTenantUrl = "http://localhost:6634/api/services/app/tenant/GetActiveTenant";



        public async Task<LoginResultDto> Login(LoginDto input)
        {
            return await Task.Run(()=> {
                var result = this._api.RealPost<AjaxResponse<string>>(Url_LoginUrl, input);
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

        public async Task<ListResultDto<TenantDto>> GetActiveTenant()
        {
            return await Task.Run(() => {
                return this._api.Post<ListResultDto<TenantDto>>(Url_GetActiveTenantUrl);
            });
        }
    }
}
