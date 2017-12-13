using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IFoxtec.Common.WPF.Services
{
    /// <summary>
    /// Grid数据线程安全服务
    /// </summary>
    public class GridDataThreadSafeService : ServiceBase, IGridDataThreadSafeService
    {
        public GridControl GridControl
        {
            get { return (GridControl)GetValue(GridControlProperty); }
            set { SetValue(GridControlProperty, value); }
        }

        public static readonly DependencyProperty GridControlProperty =
    DependencyProperty.Register("GridControl", typeof(GridControl), typeof(GridDataThreadSafeService), new PropertyMetadata(null));

        public void BeginUpdate()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                if (this.GridControl != null)
                {
                    this.GridControl.BeginDataUpdate();
                }
            }));
        }

        public void EndUpdate()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                if (this.GridControl != null)
                {
                    this.GridControl.EndDataUpdate();
                }
            }));
        }
    }
}
