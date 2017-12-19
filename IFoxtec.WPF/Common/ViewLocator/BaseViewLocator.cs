using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.WPF.Common.ViewLocator
{
    /// <summary>
    /// 基础View定位器
    /// </summary>
    public class BaseViewLocator : LocatorBase, IViewLocator
    {
        public BaseViewLocator(IEnumerable<Assembly> assemblys)
        {
            this._Assemblies = assemblys;
        }

        private IEnumerable<Assembly> _Assemblies;

        protected override IEnumerable<Assembly> Assemblies
        {
            get { return this._Assemblies; }
        }

        public string GetViewTypeName(Type type)
        {
            return type.Name;
        }

        public object ResolveView(string name)
        {
            Type t = ResolveViewType(name);
            return Activator.CreateInstance(t);
        }

        public Type ResolveViewType(string name)
        {
            foreach (var assembly in this.Assemblies)
            {
                var type = assembly.GetType(name);
                if (type != null)
                    return type;
            }

            return null;
        }
    }
}
