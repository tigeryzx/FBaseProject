using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.Core;
using System.Globalization;
using System.Threading;
using DevExpress.Mvvm.ModuleInjection;
using DevExpress.Mvvm.UI;
using System.Reflection;
using IFoxtec.Common.WPF.ViewLocator;
using Abp;
using Castle.Facilities.Logging;
using Abp.Castle.Logging.Log4Net;
using Abp.UI;

namespace IFoxtec.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly AbpBootstrapper _bootstrapper;

        public App()
        {
            this.Startup += App_Startup;
            Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            _bootstrapper = AbpBootstrapper.Create<IFoxtecWPFModule>();
            _bootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(f => f.UseAbpLog4Net().WithConfig("log4net.config"));
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _bootstrapper.Initialize();

            // 注册View供DocumentService使用
            ViewLocator.Default = new BaseViewLocator(new List<Assembly>()
            {
                typeof(App).Assembly,
            });

            base.OnStartup(e);

        }

        /// <summary>
        /// 非UI线程抛出全局异常事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var exception = e.ExceptionObject as Exception;
                if (exception != null)
                {
                    // LogHelper.Instance.Logger.Error(exception, "非UI线程全局异常");
                    var userFriendlyException = exception as UserFriendlyException;
                    if (userFriendlyException != null)
                        DXMessageBox.Show(userFriendlyException.Details, userFriendlyException.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                // LogHelper.Instance.Logger.Error(ex, "不可恢复的非UI线程全局异常");
                DXMessageBox.Show("应用程序发生不可恢复的异常，将要退出！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// UI线程抛出全局异常事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                var exception = e.Exception as Exception;
                if (exception != null)
                {
                    // LogHelper.Instance.Logger.Error(e.Exception, "UI线程全局异常");
                    var userFriendlyException = exception as UserFriendlyException;
                    if (userFriendlyException != null)
                        DXMessageBox.Show(userFriendlyException.Details, userFriendlyException.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                e.Handled = true;
            }
            catch (Exception ex)
            {
                // LogHelper.Instance.Logger.Error(ex, "不可恢复的UI线程全局异常");
                DXMessageBox.Show("应用程序发生不可恢复的异常，将要退出！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            //设置语言

            // Create a new object, representing the German culture.  
            CultureInfo culture = CultureInfo.CreateSpecificCulture("zh-Hans");

            // The following line provides localization for the application's user interface.  
            Thread.CurrentThread.CurrentUICulture = culture;

            // The following line provides localization for data formats.  
            Thread.CurrentThread.CurrentCulture = culture;

            // Set this culture as the default culture for all threads in this application.  
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }

        private void OnAppStartup_UpdateThemeName(object sender, StartupEventArgs e)
        {
            DevExpress.Xpf.Core.ApplicationThemeHelper.UpdateApplicationThemeName();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _bootstrapper.Dispose();

            base.OnExit(e);
        }
    }
}
