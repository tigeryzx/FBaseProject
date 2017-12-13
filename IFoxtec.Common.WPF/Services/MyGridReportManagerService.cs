using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Reports.UserDesigner.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IFoxtec.Common.WPF.Services
{
    public class MyGridReportManagerService : GridReportManagerService, IMyGridReportManagerService
    {
        public Window Owner
        {
            get { return (Window)GetValue(OwnerProperty); }
            set { SetValue(OwnerProperty, value); }
        }

        public static readonly DependencyProperty OwnerProperty =
DependencyProperty.Register("Owner", typeof(Window), typeof(MyGridReportManagerService), new PropertyMetadata(null));

        public void ShowReportPreview()
        {
            ((DataViewBase)this.AssociatedObject).ShowPrintPreview(this.Owner);
        }

        public void ShowReportPreviewDialog()
        {
            ((DataViewBase)this.AssociatedObject).ShowPrintPreviewDialog(this.Owner);
        }
    }
}
