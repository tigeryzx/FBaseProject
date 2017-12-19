using DevExpress.Mvvm;
using IFoxtec.WPF.Common.BaseModel;
using IFoxtec.WPF.Common.Helper;
using IFoxtec.WPF.Common.Messager;
using IFoxtec.WPF.Common.ViewModels.PartVm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.WPF.Common.ViewModels
{
    /// <summary>
    /// 基础表单 VM
    /// </summary>
    /// <typeparam name="TEntity">对象类型</typeparam>
    public class BaseFormViewModel<TEntity> : BaseDocumentViewModel
        where TEntity :new()
    {
        public BaseFormViewModel()
        {
            InitCommands();
        }

        protected override void OnInitializeInDesignMode()
        {
            if (ViewModelBase.IsInDesignMode)
            {
                this.Entity = new TEntity();
                return;
            }

            base.OnInitializeInDesignMode();
        }
 

        /// <summary>
        /// 依据父VM初始货数据
        /// </summary>
        /// <param name="parentViewModel"></param>
        protected override void OnParentViewModelChanged(object parentViewModel)
        {
            var pvm = parentViewModel as BaseDocumentViewModel;
            if (pvm != null)
            {
                this.DocTitle = pvm.DocTitle;
                this.Icon = pvm.Icon;
            }
            base.OnParentViewModelChanged(parentViewModel);
        }

        /// <summary>
        /// 参数传递
        /// </summary>
        /// <param name="parameter"></param>
        protected override void OnParameterChanged(object parameter)
        {
            var param = parameter as DocFormParam;
            if (param != null)
            {
                this.FormStatus = param.FormStatus;
                this.FormParame = param.Parame;
                RefreshTitle();
                InitData();
            }
            base.OnParameterChanged(parameter);
        }

        /// <summary>
        /// 刷新旧对象序列信息
        /// </summary>
        protected void RefreshOldEntitySerialInfo()
        {
            if (this.Entity != null)
            {
                this.OldEntitySerializeInfo = SerializeHelper.GetObjectSerializeString(this.Entity);
            }

            // 刷新标题
            this.RefreshTitle();
            // 更新命令状态
            UpdateCommands();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {

            var checkEntity = new Action<Task>((x) => {
                RefreshOldEntitySerialInfo();
            });

            if (this.FormStatus == BaseModel.FormStatus.Create)
            {
                SyncExecute(() =>
                {
                    this.Entity = OnNeedCreateEntity(this.FormParame);
                    InitSubVm(this.Entity, this.SubViewModelCollection);
                }, 
                checkEntity);
            }
            else if (this.FormStatus == BaseModel.FormStatus.Edit)
            {
                SyncExecute(() =>
                {
                    this.Entity = OnNeedEditEntity(this.FormParame);
                    InitSubVm(this.Entity, this.SubViewModelCollection);
                }, 
                checkEntity); 
            }
        }

        protected List<IBaseViewModel> SubViewModelCollection = new List<IBaseViewModel>();

        /// <summary>
        /// 初始化子VM
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="subViewModelCollection"></param>
        protected virtual void InitSubVm(TEntity entity, List<IBaseViewModel> subViewModelCollection)
        {

        }

        /// <summary>
        /// 尝试关闭文档
        /// </summary>
        /// <returns></returns>
        protected virtual bool TryClose()
        {
            bool allowClose = true;

            if (this.IsChange)
            {
                MessageResult warningResult = MessageBoxService.ShowMessage("当前修改未保存是否关闭？", "保存提示", MessageButton.OKCancel);
                allowClose = warningResult == MessageResult.OK;
            }
            return allowClose;
        }

        /// <summary>
        /// 当需要创建时的实体
        /// </summary>
        /// <returns></returns>
        protected virtual TEntity OnNeedCreateEntity(object formParame)
        {
            return new TEntity();
        }

        /// <summary>
        /// 当需要编辑时的实体
        /// </summary>
        /// <returns></returns>
        protected virtual TEntity OnNeedEditEntity(object formParame)
        {
            return default(TEntity);
        }

        /// <summary>
        /// 发送刷新信息
        /// </summary>
        protected virtual void SendReloadMessage()
        {
            var parentVm = this as ISupportParentViewModel;
            if (parentVm != null && parentVm.ParentViewModel != null)
                ((IBaseListViewModel)parentVm.ParentViewModel).SyncLoad();
        }

        /// <summary>
        /// 刷新标题
        /// </summary>
        private void RefreshTitle()
        {
            var title = string.Empty;

            if (this.FormStatus == BaseModel.FormStatus.Create)
                title = "创建";
            else if (this.FormStatus == BaseModel.FormStatus.Edit)
                title = "编辑";

            this.DocExtTitle = title;
        }

        /// <summary>
        /// 初始化命令
        /// </summary>
        protected virtual void InitCommands()
        {
            this.SaveCommand = new DelegateCommand(SaveFun, CanSave);
            this.SaveAndCloseCommand = new DelegateCommand(SaveAndCloseFun, CanSave);
            this.SaveAndCreateCommand = new DelegateCommand(SaveAndCreateFun, CanSave);
            this.ResetCommand = new DelegateCommand(ResetFun, () => { return this.IsChange; });
            this.CloseCommand = new DelegateCommand(CloseFun);
            this.DeleteCommand = new DelegateCommand(DeleteFun, CanDelete);
        }

        #region 属性

        /// <summary>
        /// 第一次加载的对象，用于判断是否保存
        /// </summary>
        private string OldEntitySerializeInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 实体内容
        /// </summary>
        public TEntity Entity
        {
            get
            {
                return GetProperty(() => this.Entity);
            }
            set
            {
                SetProperty(() => this.Entity, value);
            }
        }

        /// <summary>
        /// 表单状态
        /// </summary>
        protected FormStatus FormStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 表单参数
        /// </summary>
        protected object FormParame
        {
            get;
            set;
        }

        private bool _IsChange;

        /// <summary>
        /// 值已经改变
        /// </summary>
        protected bool IsChange
        {
            get
            {
                this._IsChange = this.Entity != null && this.OldEntitySerializeInfo != SerializeHelper.GetObjectSerializeString(this.Entity);
                RaisePropertyChanged(() => this.Title);
                return this._IsChange;
            }
        }

        /// <summary>
        /// 文档全标题
        /// </summary>
        public override string DocFullTitle
        {
            get
            {
                return base.DocFullTitle + (this._IsChange ? "*" : "");
            }
        }
        #endregion

        #region 命令

        #region 检查

        /// <summary>
        /// 更新命令状态
        /// </summary>
        protected virtual void UpdateCommands()
        {
            this.SaveCommand.RaiseCanExecuteChanged();
            this.SaveAndCloseCommand.RaiseCanExecuteChanged();
            this.SaveAndCreateCommand.RaiseCanExecuteChanged();
            this.ResetCommand.RaiseCanExecuteChanged();
            this.DeleteCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// 删除检查
        /// </summary>
        /// <returns></returns>
        protected virtual bool CanDelete()
        {
            return this.FormStatus == BaseModel.FormStatus.Edit;
        }

        /// <summary>
        /// 保存检查
        /// </summary>
        /// <returns></returns>
        protected virtual bool CanSave()
        {
            var change = this.IsChange;

            // Entity是否验证通过
            if (!this.ValidateEntity())
                return false;

            // 检查子集合是否验证通过
            if (this.SubViewModelCollection != null && this.SubViewModelCollection.Count > 0)
            {
                foreach (var vm in this.SubViewModelCollection)
                {
                    var svm = vm as ISupportValidate;
                    if (svm == null)
                        continue;
                    if (!svm.Validate())
                        return false;
                }
            }

            //创建默认允许保存
            if (this.FormStatus == BaseModel.FormStatus.Create)
                return true;


            // 检查是否跟旧对象一样
            if (this.FormStatus == BaseModel.FormStatus.Edit)
                return change;

            return false;
        }

        #endregion

        #region 保存

        /// <summary>
        /// 保存
        /// </summary>
        public DelegateCommand SaveCommand { get; set; }

        /// <summary>
        /// 保存方法
        /// </summary>
        private void SaveFun()
        {
            base.SyncExecute(() =>
            {
                return OnSave();
            }, (x) => {
                this.Entity = x.Result;

                if (this.FormStatus == BaseModel.FormStatus.Create)
                    this.FormStatus = BaseModel.FormStatus.Edit;
                this.RefreshOldEntitySerialInfo();
                this.RefreshTitle();

                SendReloadMessage();
            });
        }

        /// <summary>
        /// 当保存时
        /// </summary>
        protected virtual TEntity OnSave()
        {
            return default(TEntity);
        } 

        #endregion

        #region 保存&关闭

        /// <summary>
        /// 保存&关闭命令
        /// </summary>
        public DelegateCommand SaveAndCloseCommand { get; set; }

        private void SaveAndCloseFun()
        {
            base.SyncExecute(() =>
            {
                OnSaveAndClose();

            }, (x) =>
            {
                if (this.DispatcherService != null)
                {
                    base.DispatcherService.BeginInvoke(() =>
                    {
                        this.RefreshOldEntitySerialInfo();
                        if (TryClose())
                            base.CloseDoc();
                        SendReloadMessage();
                    });
                }
            });

        }

        /// <summary>
        /// 保存&关闭时
        /// </summary>
        /// <returns></returns>
        protected virtual void OnSaveAndClose()
        {
            OnSave();
        }

        #endregion

        #region 保存&创建

        /// <summary>
        /// 保存&创建命令
        /// </summary>
        public DelegateCommand SaveAndCreateCommand { get; set; }

        private void SaveAndCreateFun()
        {
            base.SyncExecute(() =>
            {
                OnSaveAndCreate();

            }, (x) =>
            {
                if (this.DispatcherService != null)
                {
                    base.DispatcherService.BeginInvoke(() =>
                    {
                        this.FormStatus = BaseModel.FormStatus.Create;
                        InitData();
                        this.RefreshOldEntitySerialInfo();
                        SendReloadMessage();
                    });
                }
            });
        }

        /// <summary>
        /// 保存&创建时
        /// </summary>
        /// <returns></returns>
        protected virtual void OnSaveAndCreate()
        {
            OnSave();
        }

        #endregion

        #region 撤消改变

        /// <summary>
        /// 撤消改变
        /// </summary>
        public DelegateCommand ResetCommand { get; set; }

        private void ResetFun()
        {
            OnReset();
        }

        /// <summary>
        /// 撤消改变
        /// </summary>
        /// <returns></returns>
        protected virtual void OnReset()
        {
            // TODO:思考一下，这里暂时反序列化，另一个思路是重新取新的数据
            var msgResult = base.MessageBoxService.ShowMessage("将撤消当前未保存的所有更改？", "提示", MessageButton.OKCancel);
            if (msgResult == MessageResult.OK)
            {
                //if (!string.IsNullOrEmpty(this.OldEntitySerializeInfo))
                //    this.Entity = SerializeHelper.StringToObject<TEntity>(this.OldEntitySerializeInfo);
                InitData();
            }
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除
        /// </summary>
        public DelegateCommand DeleteCommand { get; set; }

        private void DeleteFun()
        {
            if (OnDelete())
            {
                SendReloadMessage();
                CloseDoc();
            }
        }

        /// <summary>
        /// 删除时
        /// </summary>
        /// <returns></returns>
        protected virtual bool OnDelete()
        {
            return false;
        }

        #endregion

        #region 关闭

        /// <summary>
        /// 关闭
        /// </summary>
        public DelegateCommand CloseCommand { get; set; }

        private void CloseFun()
        {
            OnClose();
        }

        /// <summary>
        /// 关闭时
        /// </summary>
        /// <returns></returns>
        protected virtual void OnClose()
        {
            if (TryClose())
                CloseDoc();
        }

        #endregion

        #endregion

        #region 表单验证

        private readonly Dictionary<string, ICollection<string>> _validationErrors = new Dictionary<string, ICollection<string>>();

        /// <summary>
        /// 验证Entity对象
        /// </summary>
        /// <returns>True 需要成功</returns>
        protected bool ValidateEntity()
        {
            _validationErrors.Clear();

            if (this.Entity != null)
            {
                ICollection<ValidationResult> validationResults = new List<ValidationResult>();
                ValidationContext validationContext = new ValidationContext(this.Entity);
                // 可单独验证属性 MemberName = propertyName
                if (!Validator.TryValidateObject(this.Entity, validationContext, validationResults))
                {
                    foreach (ValidationResult validationResult in validationResults)
                    {
                        var validationKey = validationResult.MemberNames.First();
                        if (!this._validationErrors.Keys.Contains(validationKey))
                            this._validationErrors.Add(validationKey, new List<string>());
                        _validationErrors[validationKey].Add(validationResult.ErrorMessage);
                    }
                }
            }
            else
                return false;

            return this._validationErrors == null || this._validationErrors.Count <= 0;
        }

        #endregion

        #region IDocumnet 实现重写

        public override void OnClose(CancelEventArgs e)
        {
            if (!TryClose())
                e.Cancel = true;
            base.OnClose(e);
        }

        #endregion
    }
}
