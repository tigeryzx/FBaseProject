using Abp.Localization;
using Abp.Modules;
using Abp.Zero.Configuration;
using IFoxtec.Common.WPF;
using System.Reflection;

namespace IFoxtec.WPF
{
    [DependsOn(
        typeof(IFoxtecCommonWPFModule),
        typeof(IFoxtecDataModule),
        typeof(IFoxtecApplicationModule))]
    public class IFoxtecWPFModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Enable database based localization
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            base.PreInitialize();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
