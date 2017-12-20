using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IFoxtec.Facade.WebApi;
using IFoxtec.Sessions.Dto;
using IFoxtec.WPF.Common.Contract;

namespace IFoxtec.WPF.Module.Account
{
    public class SessionContract : BaseContract,ISessionContract
    {
        ApiHelper _api;

        private const string Url_GetCurrentLoginInformationsOutput = "http://localhost:6634/api/services/app/session/GetCurrentLoginInformations";

        public SessionContract()
        {
            this._api = new ApiHelper();
        }

        public Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            return Task.Run(() => {
                return this._api.Post<GetCurrentLoginInformationsOutput>(Url_GetCurrentLoginInformationsOutput);
            });
        }
    }
}
