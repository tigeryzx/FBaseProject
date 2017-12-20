using IFoxtec.Sessions.Dto;
using IFoxtec.WPF.Common.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.WPF.Module.Account
{
    public interface ISessionContract : IContract
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
