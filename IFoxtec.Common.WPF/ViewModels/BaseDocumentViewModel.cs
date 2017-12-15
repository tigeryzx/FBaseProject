using DevExpress.Mvvm;
using IFoxtec.Common.WPF.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace IFoxtec.Common.WPF.ViewModels
{
    /// <summary>
    /// 文档基础VM
    /// </summary>
    public class BaseDocumentViewModel : BaseViewModel, ISupportParameter, IDocumentContent, ISupportParentViewModel, IBaseViewModel
    {
        static object SyncRoot = new object();

        #region 服务

        /// <summary>
        /// 文档服务
        /// </summary>
        protected IDocumentManagerService DocumentManagerService
        {
            get
            {
                return base.GetService<IDocumentManagerService>();
            }
        }


        /// <summary>
        /// 信息框服务
        /// </summary>
        protected IMessageBoxService MessageBoxService
        {
            get
            {
                return base.GetService<IMessageBoxService>();
            }
        }

        /// <summary>
        /// 线程调试服务
        /// </summary>
        protected IDispatcherService DispatcherService
        {
            get
            {
                return base.GetService<IDispatcherService>();
            }
        }

        #endregion

        #region 属性

        /// <summary>
        /// 图标
        /// </summary>
        public ImageSource Icon
        {
            get
            {
                return GetProperty(() => this.Icon);
            }
            set
            {
                SetProperty(() => this.Icon, value);
            }
        }

        /// <summary>
        /// 标题后缀
        /// </summary>
        public string DocExtTitle
        {
            get
            {
                return GetProperty(() => this.DocExtTitle);
            }
            set
            {
                SetProperty(() => this.DocExtTitle, value);
                base.RaisePropertyChanged(() => this.Title);
            }
        }

        /// <summary>
        /// 文档标题
        /// </summary>
        public string DocTitle
        {
            get
            {
                return GetProperty(()=>this.DocTitle);
            }
            set
            {
                SetProperty(() => this.DocTitle, value);
                base.RaisePropertyChanged(() => this.Title);
            }
        }

        /// <summary>
        /// 文档全标题
        /// </summary>
        public virtual string DocFullTitle
        {
            get
            {
                if (!string.IsNullOrEmpty(this.DocExtTitle))
                    return string.Format("{0}[{1}]", this.DocTitle, this.DocExtTitle);
                return this.DocTitle;
            }
        }

        /// <summary>
        /// 是否加载中
        /// </summary>
        public bool IsLoading
        {
            get
            {
                return GetProperty(() => this.IsLoading);
            }
            set
            {
                SetProperty(() => this.IsLoading, value);
            }
        }

        #endregion

        #region 线程操作

        /// <summary>
        /// 异步线程操作
        /// </summary>
        /// <param name="action">执行操作</param>
        /// <param name="callBackFun">完成回调</param>
        /// <returns></returns>
        protected CancellationTokenSource SyncExecute(Action action, Action<Task> callBackFun = null)
        {
            this.IsLoading = true;

            // http://www.cnblogs.com/TianFang/archive/2012/08/03/2620841.html
            // 创建一个可以取消的异步任务，详情可参考以上

            var cancellationTokenSource = new CancellationTokenSource();
            Task.Factory.StartNew(() =>
            {
                lock (SyncRoot)
                {
                    action();
                }

            }).ContinueWith(x =>
            {
                if (callBackFun != null)
                {
                    lock (SyncRoot)
                    {
                        if (callBackFun != null)
                            callBackFun(x);
                    }
                }
                this.IsLoading = false;

                if (x.IsFaulted)
                    ShowErrorMsg(x);

            }, cancellationTokenSource.Token, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

            return cancellationTokenSource;
        }

        /// <summary>
        /// 异步线程操作
        /// </summary>
        /// <param name="action">执行操作</param>
        /// <param name="callBackFun">完成回调</param>
        /// <returns></returns>
        protected CancellationTokenSource SyncExecute<TResult>(Func<TResult> func, Action<Task<TResult>> callBackFun = null)
        {
            this.IsLoading = true;

            // http://www.cnblogs.com/TianFang/archive/2012/08/03/2620841.html
            // 创建一个可以取消的异步任务，详情可参考以上

            var cancellationTokenSource = new CancellationTokenSource();
            Task.Factory.StartNew(() =>
            {
                lock (SyncRoot)
                {
                    return func();
                }

            }).ContinueWith(x =>
            {
                if (callBackFun != null)
                {
                    if (callBackFun != null)
                        callBackFun(x);
                }
                this.IsLoading = false;

                if (x.IsFaulted)
                    ShowErrorMsg(x);

            }, cancellationTokenSource.Token, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

            return cancellationTokenSource;
        }

        /// <summary>
        /// 显示错误信息
        /// </summary>
        private void ShowErrorMsg(Task task)
        {
            if (this.MessageBoxService != null)
                this.MessageBoxService.ShowMessage("操作失败：" + task.Exception.InnerException.Message, "错误", MessageButton.OK, MessageIcon.Error);

        }
        #endregion


        #region 文档操作

        /// <summary>
        /// 关闭文档
        /// </summary>
        protected void CloseDoc()
        {
            if (DocumentOwner != null)
                this.DocumentOwner.Close(this);
        }

        #endregion

        #region IDocumentContent 实现

        public IDocumentOwner DocumentOwner
        {
            get;
            set;
        }

        public virtual void OnClose(System.ComponentModel.CancelEventArgs e)
        {

        }

        public void OnDestroy()
        {

        }

        public object Title
        {
            get { return this.DocFullTitle; }
        } 
        #endregion
    }
}
