using Autofac;
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
    public class TypeRegister
    {
        public static IContainer Container { get; set; }

        public static void Register(Assembly contractAssembly,List<Assembly> viewModelAssembly)
        {
            var builder = new ContainerBuilder();
            foreach (var item in viewModelAssembly)
            {
                builder.RegisterAssemblyTypes(item)
                    .Where(type => typeof(IBaseViewModel).IsAssignableFrom(type) && !type.IsAbstract)
                    .AsSelf()
                    .InstancePerDependency();
            }

            builder.RegisterAssemblyTypes(contractAssembly)
                .AsImplementedInterfaces()
                .InstancePerDependency();

            Container = builder.Build();
        }

    }
}
