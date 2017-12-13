using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.UI;
using IFoxtec.Common.WPF.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Autofac;

namespace IFoxtec.Common.WPF.Extensions
{
    public class MyViewModelSourceExtension : MarkupExtension
    {
	    public Type Type
		{
			get;
			set;
		}

		public MyViewModelSourceExtension()
		{
		}

        public MyViewModelSourceExtension(Type type)
		{
			this.Type = type;
		}

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			if (!(this.Type != null))
			{
				return null;
			}
            var result = TypeRegister.Container.Resolve(this.Type);

            return result;
		}
    }
}
