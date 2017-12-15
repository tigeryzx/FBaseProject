using Abp.AutoMapper;
using Abp.Dependency;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using IFoxtec.Facade.Account;
using IFoxtec.WPF.Module.Account.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Deployment.Application;
using System.Linq;

namespace IFoxtec.WPF.Module.Account
{
    /// <summary>
    /// 登录VM
    /// </summary>
    [POCOViewModel(ImplementIDataErrorInfo = true)]
    public class LoginViewModel : ViewModelBase
    {
        public static LoginViewModel Create()
        {
            var account = IocManager.Instance.Resolve<IAccountContract>();
            return ViewModelSource.Create(() => new LoginViewModel(account));
        }

        private readonly IAccountContract _accountContract;

        public LoginViewModel(IAccountContract accountContract)
        {
            this._accountContract = accountContract;

            InitCommand();

            InitData();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void InitData()
        {
            //应用版本
            if (ApplicationDeployment.IsNetworkDeployed)
                this.AppVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            else
                this.AppVersion = "未部署版本";
        }

        #region 属性

        /// <summary>
        /// 应用版本
        /// </summary>
        public string AppVersion
        {
            get;
            set;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "用户名为必须项")]
        public string UsernameOrEmailAddress
        {
            get
            {
                return GetProperty(() => this.UsernameOrEmailAddress);
            }
            set
            {
                SetProperty(() => this.UsernameOrEmailAddress, value);
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码为必须项")]
        public string Password
        {
            get
            {
                return GetProperty(() => this.Password);
            }
            set
            {
                SetProperty(() => this.Password, value);
            }
        }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string ResultMessage
        {
            get
            {
                return GetProperty(() => this.ResultMessage);
            }
            set
            {
                SetProperty(() => this.ResultMessage, value);
            }
        }

        /// <summary>
        /// 登录失败
        /// </summary>
        public bool LoginFailed
        {
            get
            {
                return GetProperty(() => this.LoginFailed);
            }
            set
            {
                SetProperty(() => this.LoginFailed, value);
            }
        }

        /// <summary>
        /// 租户类型列表
        /// </summary>
        public IEnumerable<TenantItemModel> TenantList
        {
            get
            {
                return GetProperty(() => this.TenantList);
            }
            set
            {
                SetProperty(() => this.TenantList, value);
            }
        }


        /// <summary>
        /// 选择租户
        /// </summary>
        [Required(ErrorMessage = "租户为必须项")]
        public TenantItemModel SelTenant
        {
            get
            {
                return GetProperty(() => this.SelTenant);
            }
            set
            {
                SetProperty(() => this.SelTenant, value);
            }
        }

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

        #region 服务

        protected IMessageBoxService MessageBoxService
        {
            get
            {
                return base.GetService<IMessageBoxService>();
            }
        }

        protected ICurrentWindowService CurrentWindowService
        {
            get
            {
                return base.GetService<ICurrentWindowService>();
            }
        }
        #endregion

        #region 命令

        /// <summary>
        /// 登录命令
        /// </summary>
        public DelegateCommand LoginCommand { get; set; }

        public DelegateCommand CloseCommand { get; set; }

        public DelegateCommand LoadDataCommand { get; set; }

        public Action GoToMainWin { get; set; }

        private void InitCommand()
        {
            this.LoginCommand = new DelegateCommand(Login, CanLogin);
            this.CloseCommand = new DelegateCommand(Close);
            this.LoadDataCommand = new DelegateCommand(LoadData);
        }

        private async void LoadData()
        {

            this.IsLoading = true;
            var tenantList = await this._accountContract.GetActiveTenant();

            this.TenantList = tenantList.Items.MapTo<List<TenantItemModel>>();
            if (this.TenantList != null && this.TenantList.Count() > 0)
                this.SelTenant = this.TenantList.FirstOrDefault();

            this.IsLoading = false;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        private void Close()
        {
            this.CurrentWindowService.Close();
        }

        /// <summary>
        /// 登录
        /// </summary>
        protected async void Login()
        {
            if (this.GoToMainWin == null)
                this.MessageBoxService.ShowMessage("未设置登录回调", "提示");
            else
            {
                var resultInfo = await this._accountContract.Login(new Facade.Account.Dto.LoginDto() {
                    UsernameOrEmailAddress = this.UsernameOrEmailAddress,
                    TenancyName = this.SelTenant.TenancyName,
                    Password = this.Password
                });
                if (resultInfo.Success)
                    this.GoToMainWin();
                else
                    this.ResultMessage = resultInfo.ErrorMessage + "：" + resultInfo.ErrorDetails;
                this.LoginFailed = !resultInfo.Success;

            }
        }

        /// <summary>
        /// 是否允许登录
        /// </summary>
        protected bool CanLogin()
        {
            if (string.IsNullOrEmpty(this.UsernameOrEmailAddress) || string.IsNullOrEmpty(this.Password) || this.SelTenant ==null)
                return false;
            return true;
        }

        #endregion
    }
}
