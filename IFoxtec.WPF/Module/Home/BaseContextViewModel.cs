using Abp.Application.Services.Dto;
using DevExpress.Images;
using DevExpress.Mvvm;
using DevExpress.Utils.Design;
using DevExpress.Xpf.Core;
using IFoxtec.WPF.Common.BaseModel;
using IFoxtec.WPF.Common.UIThread;
using IFoxtec.WPF.Module.Account;
using IFoxtec.WPF.Module.Users;
using System.Collections.Generic;

namespace IFoxtec.WPF.Module.Home
{
    public class BaseContextViewModel : BaseHomeViewModel
    {
        private readonly ISessionContract _sessionContract;

        public BaseContextViewModel(ISessionContract sessionContract)
        {
            this._sessionContract = sessionContract;

            InitCommand();

            InitSampleData();
        }

        private void InitSampleData()
        {
            // TODO:之后改成从专门的数据层中获取数据填充
            this.ModuleList = new List<ModuleDescription>() 
            { 
                new ModuleDescription("模块A",new List<MenuDescription>()
                {
                    new MenuDescription("用户管理") { DocumnetType = PageViewIndex.UserListView ,Icon = DXImageHelper.GetImageSource(DXImages.Currency, ImageSize.Size16x16)},
                    new MenuDescription("角色管理") ,
                    new MenuDescription("测试页面")
                }),
                new ModuleDescription("模块B",new List<MenuDescription>()
                {
                    new MenuDescription("功能菜单4"),
                    new MenuDescription("功能菜单5")
                })
            };

            // 获取用户信息
            ThreadHelper.StartTaskAndCallbackUI(() =>
            {
                return this._sessionContract.GetCurrentLoginInformations().Result;
            }
            ,(x)=> 
            {
                this.CurrentUser = "当前用户：" + x.Result.User.UserName;
            });
            
        }

        #region 属性

        /// <summary>
        /// 当前用户
        /// </summary>
        public string CurrentUser
        {
            get
            {
                return GetProperty(() => this.CurrentUser);
            }
            protected set
            {
                SetProperty(() => this.CurrentUser, value);
            }
        }

        #endregion

        #region 命令

        /// <summary>
        /// 初始化命令
        /// </summary>
        private void InitCommand()
        {
            this.SimpleCommand = new DelegateCommand<string>(SimpleFun);
        }

        /// <summary>
        /// 测试命令
        /// </summary>
        public DelegateCommand<string> SimpleCommand { get; set; }

        private void SimpleFun(string parame)
        {
            this.MessageBoxService.ShowMessage(parame);
        } 
        #endregion
    }
}