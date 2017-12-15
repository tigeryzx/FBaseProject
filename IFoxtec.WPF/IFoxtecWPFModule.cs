using Abp.Modules;
using System.Reflection;

namespace IFoxtec.WPF
{
    [DependsOn(typeof(IFoxtecDataModule),typeof(IFoxtecApplicationModule))]
    public class IFoxtecWPFModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
