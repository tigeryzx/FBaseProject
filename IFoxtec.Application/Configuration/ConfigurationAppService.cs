using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using IFoxtec.Configuration.Dto;

namespace IFoxtec.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : IFoxtecAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
