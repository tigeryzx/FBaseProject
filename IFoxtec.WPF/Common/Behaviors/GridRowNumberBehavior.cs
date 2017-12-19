using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.WPF.Common.Behaviors
{
    /// <summary>
    /// 表格行号 行为
    /// </summary>
    public class GridRowNumberBehavior : Behavior<GridControl>
    {
        GridControl AssociatedGridControl { get { return AssociatedObject; } }

        public string _ColumnName = "#";

        /// <summary>
        /// 行号名称，默认为 #
        /// </summary>
        public string ColumnName
        {
            get
            {
                return this._ColumnName;
            }
            set
            {
                this._ColumnName = value;
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedGridControl.CustomUnboundColumnData += AssociatedGridControl_CustomUnboundColumnData;

            var noCol = new GridColumn();
            noCol.FieldName = this.ColumnName;
            noCol.Visible = true;
            noCol.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            noCol.AllowEditing = DevExpress.Utils.DefaultBoolean.False;
            noCol.AllowGrouping = DevExpress.Utils.DefaultBoolean.False;
            noCol.AllowMoving = DevExpress.Utils.DefaultBoolean.False;
            noCol.AllowSearchPanel = DevExpress.Utils.DefaultBoolean.False;
            noCol.AllowAutoFilter = false;
            noCol.AllowColumnFiltering = DevExpress.Utils.DefaultBoolean.False;
            noCol.AllowBestFit = DevExpress.Utils.DefaultBoolean.True;
            noCol.ReadOnly = true;
            noCol.Width = 50;
            noCol.AllowCellMerge = false;
            noCol.Fixed = FixedStyle.Left;

            AssociatedGridControl.Columns.Insert(0, noCol);
        }

        void AssociatedGridControl_CustomUnboundColumnData(object sender, GridColumnDataEventArgs e)
        {
            if (e.IsGetData)
                e.Value = e.ListSourceRowIndex + 1;
        }
    }
}
