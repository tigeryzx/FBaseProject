using System.Threading.Tasks;
using Abp.Application.Services;
using IFoxtec.Configuration.Dto;

namespace IFoxtec.Configuration
{
    public interface IConfigurationAppService: IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}