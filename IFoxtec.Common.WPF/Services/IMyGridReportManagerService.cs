using DevExpress.Xpf.Reports.UserDesigner.Extensions;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.Common.WPF.Services
{
    public interface IMyGridReportManagerService
    {
        XtraReport GenerateReport(XtraReport initialReport = null);
        void AssignDataSource(XtraReport report);
        ReportInfo SaveReport(XtraReport report);
        void RemoveReport(ReportInfo info);
        XtraReport GetReport(ReportInfo info);
        void UpdateReport(ReportInfo info, XtraReport report);
        IEnumerable<ReportInfo> GetReports();
        ReportManagerServiceViewModel ViewModel { get; }
        bool HasPreview { get; }
        event EventHandler ReportsChanged;
        void ShowReportPreview();

        void ShowReportPreviewDialog();
    }
}
