using Abp.Modules;
using IFoxtec.Common.WPF;
using IFoxtec.Facade;
using IFoxtec.Facade.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.WPF
{
    [DependsOn(
        typeof(IFoxtecCommonWPFModule),
        typeof(IFoxtecFacadeModule),
        typeof(IFoxtecFacadeWebApiModule),
        typeof(IFoxtecApplicationModule))]
    public class IFoxtecWPFModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
