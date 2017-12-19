using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Collections.Generic;
using DevExpress.Xpf.Core;
using DevExpress.Images;
using DevExpress.Utils.Design;
using IFoxtec.Common.WPF.BaseModel;
using IFoxtec.WPF.Common.ViewModels;
using IFoxtec.WPF.Common.BaseModel;

namespace IFoxtec.WPF.Module.Home
{
    public class BaseContextViewModel : BaseHomeViewModel
    {
        public BaseContextViewModel()
        {
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
                    new MenuDescription("用户管理") /*{ DocumnetType = SysViewsIndex.UserList ,Icon = DXImageHelper.GetImageSource(DXImages.Currency, ImageSize.Size16x16)}*/,
                    new MenuDescription("角色管理") ,
                    new MenuDescription("测试页面")
                }),
                new ModuleDescription("模块B",new List<MenuDescription>()
                {
                    new MenuDescription("功能菜单4"),
                    new MenuDescription("功能菜单5")
                })
            };
        }

        #region 属性

        /// <summary>
        /// 当前用户
        /// </summary>
        public string CurrentUser
        {
            get
            {
                return "当前用户：";
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