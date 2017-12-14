using IFoxtec.Facade.Account;
using IFoxtec.Facade.Account.Dto;
using IFoxtec.Facade.WebApi.Contract.Common;
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

        public AccountContract()
        {
            this._api = new ApiHelper();
        }

        public const string LoginUrl = "http://localhost:6634/api/Account/Authenticate";

        public Task<LoginResultDto> Login(LoginDto input)
        {
            return Task.Run(()=> {
                var result = this._api.SrcPost(LoginUrl, input);
                var loginResult = new LoginResultDto()
                {
                    Success = result.Success,
                };

                if(!result.Success && result.Error != null)
                {
                    loginResult.ErrorMessage = result.Error.Message;
                    loginResult.ErrorDetails = result.Error.Details;
                }
                return loginResult;
            });
        }
    }
}
