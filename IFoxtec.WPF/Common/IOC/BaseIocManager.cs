using Castle.MicroKernel.Registration;
using Castle.Windsor;
using IFoxtec.Facade.WebApi;
using IFoxtec.WPF.Common.Config;
using IFoxtec.WPF.Common.Contract;
using IFoxtec.WPF.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.WPF.Common.IOC
{
    public class BaseIocManager
    {
        public static BaseIocManager _baseIocManager;

        protected WindsorContainer _container;

        protected BaseIocManager()
        {
            this._container = new WindsorContainer();

            // 注册契约
            this._container.Register(Classes.FromThisAssembly()
                .BasedOn<BaseContract>()
                .WithServiceDefaultInterfaces()
                .LifestyleTransient());

            // 注册ViewModel
            this._container.Register(Classes.FromThisAssembly()
                .BasedOn<BaseViewModel>()
                .WithServiceSelf()
                .LifestyleTransient());

            // 注册配置管理器
            this._container.Register(Classes.From(typeof(MemoryConfigManager))
                .Pick()
                .WithServices(typeof(IConfigManager))
                .LifestyleSingleton());
        }

        public static BaseIocManager Instance
        {
            get
            {
                if (_baseIocManager == null)
                    _baseIocManager = new BaseIocManager();
                return _baseIocManager;
            }
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
