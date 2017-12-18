using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Runtime.Security;
using Abp.UI;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using IFoxtec.Authorization;
using IFoxtec.Common.WPF.Config;
using IFoxtec.Common.WPF.ViewModels;
using IFoxtec.MultiTenancy;
using IFoxtec.WPF.Module.Account.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Deployment.Application;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace IFoxtec.WPF.Module.Account
{
    /// <summary>
    /// 登录VM
    /// </summary>
    [POCOViewModel(ImplementIDataErrorInfo = true)]
    public class LoginViewModel : BaseViewModel
    {
        public static LoginViewModel Create()
        {
            var tenantAppService = IocManager.Instance.Resolve<ITenantAppService>();
            var logInManager = IocManager.Instance.Resolve<LogInManager>();
            var configManager = IocManager.Instance.Resolve<IConfigManager>();

            return ViewModelSource.Create(() => new LoginViewModel(tenantAppService, logInManager, configManager));
        }

        private readonly ITenantAppService _tenantAppService;
        private readonly LogInManager _logInManager;
        private readonly IConfigManager _configManager;

        public LoginViewModel(
            ITenantAppService tenantAppService,
            LogInManager logInManager,
            IConfigManager configManager)
        {
            this._tenantAppService = tenantAppService;
            this._logInManager = logInManager;
            this._configManager = configManager;

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
        public AsyncCommand LoginCommand { get; set; }

        public DelegateCommand CloseCommand { get; set; }

        public AsyncCommand LoadDataCommand { get; set; }

        public Action GoToMainWin { get; set; }

        private void InitCommand()
        {
            this.LoginCommand = new AsyncCommand(Login, CanLogin);
            this.CloseCommand = new DelegateCommand(Close);
            this.LoadDataCommand = new AsyncCommand(LoadData);
        }
        
        private Task LoadData()
        {
            return Task.Factory.StartNew(() =>
            {
                this.IsLoading = true;

                var tenantList = this._tenantAppService.GetActiveTenant().Result;
                this.TenantList = tenantList.Items.MapTo<List<TenantItemModel>>();
                if (this.TenantList != null && this.TenantList.Count() > 0)
                    this.SelTenant = this.TenantList.FirstOrDefault();

                this.IsLoading = false;

            });

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
        protected Task Login()
        {
            return Task.Factory.StartNew(()=> {

                this.IsLoading = true;
                this.LoginFailed = false;

                if (this.GoToMainWin == null)
                    this.MessageBoxService.ShowMessage("未设置登录回调", "提示");
                else
                {
                    return this._logInManager.LoginAsync(
                        this.UsernameOrEmailAddress,
                        this.Password,
                        this.SelTenant.TenancyName).Result;
                }

                return null;

            }).ContinueWith(x=> {
                this.IsLoading = false;

                var loginResult = x.Result;
                switch (loginResult.Result)
                {
                    case AbpLoginResultType.Success:
                        {
                            var user = loginResult.User;

                            ClaimsIdentity identity = new ClaimsIdentity();
                            identity.AddClaim(new Claim(AbpClaimTypes.UserId, user.Id.ToString(CultureInfo.InvariantCulture)));
                            identity.AddClaim(new Claim(AbpClaimTypes.UserName, user.UserName));
                            identity.AddClaim(new Claim(AbpClaimTypes.TenantId, user.TenantId.Value.ToString(CultureInfo.InvariantCulture)));

                            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                            Thread.CurrentPrincipal = principal;

                            this.GoToMainWin();
                            break;
                        }
                    default:
                        {
                            throw CreateExceptionForFailedLoginAttempt(loginResult.Result, this.UsernameOrEmailAddress, this.SelTenant.TenancyName);
                        }
                }
                this.LoginFailed = loginResult.Result != AbpLoginResultType.Success;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private Exception CreateExceptionForFailedLoginAttempt(AbpLoginResultType result, string usernameOrEmailAddress, string tenancyName)
        {
            switch (result)
            {
                case AbpLoginResultType.Success:
                    return new ApplicationException("Don't call this method with a success result!");
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                case AbpLoginResultType.InvalidPassword:
                    return new UserFriendlyException(L("LoginFailed"), L("InvalidUserNameOrPassword"));
                case AbpLoginResultType.InvalidTenancyName:
                    return new UserFriendlyException(L("LoginFailed"), L("ThereIsNoTenantDefinedWithName{0}", tenancyName));
                case AbpLoginResultType.TenantIsNotActive:
                    return new UserFriendlyException(L("LoginFailed"), L("TenantIsNotActive", tenancyName));
                case AbpLoginResultType.UserIsNotActive:
                    return new UserFriendlyException(L("LoginFailed"), L("UserIsNotActiveAndCanNotLogin", usernameOrEmailAddress));
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    return new UserFriendlyException(L("LoginFailed"), "UserEmailIsNotConfirmedAndCanNotLogin");
                case AbpLoginResultType.LockedOut:
                    return new UserFriendlyException(L("LoginFailed"), L("UserLockedOutMessage"));
                default: //Can not fall to default actually. But other result types can be added in the future and we may forget to handle it
                    //Logger.Warn("Unhandled login fail reason: " + result);
                    return new UserFriendlyException(L("LoginFailed"));
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
