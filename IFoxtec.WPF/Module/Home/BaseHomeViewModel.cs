using Abp.Runtime.Session;
using DevExpress.Images;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Utils.Design;
using DevExpress.Xpf.Core;
using IFoxtec.Common.WPF.BaseModel;
using IFoxtec.Common.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.WPF.Module.Home
{
    /// <summary>
    /// 基础主页VM
    /// </summary>
    public class BaseHomeViewModel : BaseViewModel
    {
        public BaseHomeViewModel()
        {

        }

        private void InitCommands()
        {
            this.OpenMenuCommand = new DelegateCommand<MenuDescription>(OpenMenuFun);
            this.LockCommand = new DelegateCommand(LockFun);
        }

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
        /// 对话框服务
        /// </summary>
        protected IDialogService DialogService
        {
            get
            {
                return base.GetService<IDialogService>();
            }
        }
        #endregion

        #region 属性

        /// <summary>
        /// 功能模块列表
        /// </summary>
        public List<ModuleDescription> ModuleList
        {
            get
            {
                return GetProperty(() => this.ModuleList);
            }
            protected set
            {
                SetProperty(() => this.ModuleList, value);
            }
        }

        /// <summary>
        /// 选中模块
        /// </summary>
        public MenuDescription SelectedModule
        {
            get
            {
                return GetProperty(() => this.SelectedModule);
            }
            set
            {
                SetProperty(() => this.SelectedModule, value);
            }
        }

        /// <summary>
        /// 当前激活模块
        /// </summary>
        public MenuDescription ActiveModule
        {
            get
            {
                return GetProperty(() => this.ActiveModule);
            }
            set
            {
                SetProperty(() => this.ActiveModule, value);
            }
        } 
        #endregion

        #region 命令

        /// <summary>
        /// 打开菜单命令
        /// </summary>
        public DelegateCommand<MenuDescription> OpenMenuCommand { get; set; }

        /// <summary>
        /// 打开菜单
        /// </summary>
        /// <param name="menuDesc"></param>
        private void OpenMenuFun(MenuDescription menuDesc)
        {
            if (string.IsNullOrEmpty(menuDesc.DocumnetType))
            {
                MessageBoxService.ShowMessage("文档未设置");
                return;
            }

            var document = this.DocumentManagerService.FindDocumentByIdOrCreate(menuDesc.DocumnetType, (service) => 
            {
                return service.CreateDocument(menuDesc.DocumnetType,menuDesc, this);
            });
            
            document.Title = menuDesc.MenuTitle;
            document.DestroyOnClose = true;
            document.Show();
        }

        /// <summary>
        /// 锁屏命令
        /// </summary>
        public DelegateCommand LockCommand { get; set; }

        /// <summary>
        /// 锁屏
        /// </summary>
        private void LockFun()
        {
            LockViewModel lockVm = ViewModelSource.Create(() => new LockViewModel());

            UICommand submitCommand = new UICommand()
            {
                Caption = "确认",
                IsCancel = false,
                IsDefault = true,
                Command = new DelegateCommand<CancelEventArgs>(
                    x =>
                    {
                        x.Cancel = !lockVm.IsPass;
                    }
                    ,
                    x => lockVm.IsPass
                    )
            };

            this.DialogService.ShowDialog(new List<UICommand>() { submitCommand }, "锁屏", lockVm);

        }

        #endregion
    }
}
