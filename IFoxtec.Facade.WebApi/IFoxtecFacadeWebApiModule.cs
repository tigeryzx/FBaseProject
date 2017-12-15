using Abp.Modules;
using IFoxtec.Common.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.Facade.WebApi
{
    [DependsOn(
        typeof(IFoxtecCommonWPFModule), 
        typeof(IFoxtecFacadeModule), 
        typeof(IFoxtecApplicationModule))]
    public class IFoxtecFacadeWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
