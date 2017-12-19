using DevExpress.Mvvm.UI;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IFoxtec.WPF.Common.Controls.Layout
{
    /// <summary>
    /// 基础Dock布局
    /// </summary>
    public class BaseDockLayout : BaseLayout
    {
        public BaseDockLayout()
        {
            this.DefaultStyleKey = typeof(BaseDockLayout);

            SetGetBehaviors();
        }

        private void SetGetBehaviors()
        {
            DXMessageBoxService dxMessageBoxService = new DXMessageBoxService();
            Interaction.GetBehaviors(this).Add(dxMessageBoxService);

            WindowedDocumentUIService windowedDocumentUIService = new WindowedDocumentUIService();
            windowedDocumentUIService.YieldToParent = true;
            Interaction.GetBehaviors(this).Add(windowedDocumentUIService);

            DispatcherService dispatcherService = new DispatcherService();
            Interaction.GetBehaviors(this).Add(dispatcherService);

        }

        #region 属性

        /// <summary>
        /// 头部内容
        /// </summary>
        public object TopContent
        {
            get { return GetValue(TopContentProperty); }
            set { SetValue(TopContentProperty, value); }
        }

        public static readonly DependencyProperty TopContentProperty =
            DependencyProperty.Register("TopContent", typeof(object), typeof(BaseLayout), null);

        /// <summary>
        /// 底部内容
        /// </summary>
        public object BottomContent
        {
            get { return GetValue(BottomContentProperty); }
            set { SetValue(BottomContentProperty, value); }
        }

        public static readonly DependencyProperty BottomContentProperty =
            DependencyProperty.Register("BottomContent", typeof(object), typeof(BaseLayout), null);

        /// <summary>
        /// 主区域内容
        /// </summary>
        public object MainContent
        {
            get { return GetValue(MainContentProperty); }
            set { SetValue(MainContentProperty, value); }
        }

        public static readonly DependencyProperty MainContentProperty =
            DependencyProperty.Register("MainContent", typeof(object), typeof(BaseLayout), null);


        /// <summary>
        /// 是否加载中
        /// </summary>
        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register("IsLoading", typeof(bool), typeof(BaseLayout), null);
        #endregion
    }
}
