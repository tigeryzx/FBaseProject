using System.Threading.Tasks;
using Abp.Application.Services;
using IFoxtec.Sessions.Dto;

namespace IFoxtec.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
