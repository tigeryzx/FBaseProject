using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.UI;
using IFoxtec.Common.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.Common.WPF.IOC
{
    public class IOCManager
    {
        private static IWindsorContainer container = new WindsorContainer();

        public static void Register(Assembly contractAssembly, List<Assembly> viewModelAssembly)
        {
            // 注册ViewModel
            foreach (var assembly in viewModelAssembly)
            {
                container.Register(Classes.FromAssembly(assembly)
                    .BasedOn<IBaseViewModel>()
                    .WithServiceSelf());
            }

            container.Register(Classes.FromAssembly(contractAssembly)
                .Pick()
                .WithServiceDefaultInterfaces());

        }

        public static TResult Resolve<TResult>()
        {
            return container.Resolve<TResult>();
        }

        public static object Resolve(Type type)
        {
            return container.Resolve(type);
        }
    }
}
