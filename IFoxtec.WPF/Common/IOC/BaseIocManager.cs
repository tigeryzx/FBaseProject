using Castle.MicroKernel.Registration;
using Castle.Windsor;
using IFoxtec.Common.WPF.Config;
using IFoxtec.Facade.WebApi;
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
            this._container.Install(new WebApiFacadeInstaller());

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

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
