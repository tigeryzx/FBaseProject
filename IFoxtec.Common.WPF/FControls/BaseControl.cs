using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace IFoxtec.WPF.Common.FControls
{
    public class BaseControl :Control
    {
        public BaseControl()
        {
            this.DefaultStyleKey = typeof(BaseControl);
        }
    }
}
