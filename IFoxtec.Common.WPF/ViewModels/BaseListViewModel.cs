using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Reports.UserDesigner.Extensions;
using IFoxtec.WPF.Common.BaseModel;
using IFoxtec.WPF.Common.Messager;
using IFoxtec.WPF.Common.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IFoxtec.WPF.Common.ViewModels
{
    /// <summary>
    /// 列表页基础VM
    /// </summary>
    /// <typeparam name="TEntity">对象类型</typeparam>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    public abstract class BaseListViewModel<TEntity, TPrimaryKey> : 
        BaseDocumentViewModel, IBaseListViewModel
        where TEntity : BaseEntity<TPrimaryKey>
        where TPrimaryKey :struct
    {
        /// <summary>
        /// 加载数据源
        /// </summary>
        public abstract ObservableCollection<TEntity> LoadEntities();

        protected ObservableCollection<TEntity> entities;

        /// <summary>
        /// 异步加载
        /// </summary>
        /// <returns></returns>
        public CancellationTokenSource SyncLoad()
        {
            var selectedEntityCallback = GetSelectedEntityCallback();

            return SyncExecute<ObservableCollection<TEntity>>(() =>
            {
                return this.LoadEntities();
            },
            (x)=>
            {
                if (!x.IsFaulted)
                {
                    this.entities = x.Result;
                    base.RaisePropertyChanged(() => this.Entities);
                    this.OnEntitiesAssigned(selectedEntityCallback);
                }
            });
        }

        public BaseListViewModel()
        {
            InitCommands();
            // https://www.devexpress.com/Support/Center/Example/Details/E3139/mvvm-how-to-bind-the-gridcontrol-s-selected-rows-to-a-property-in-a-viewmodel
        }

        protected virtual void InitCommands()
        {
            this.CreateCommand = new DelegateCommand(CreateFun, CanUseCommand);
            this.EditCommand = new DelegateCommand(EditFun, CanExecuteNeedRecordFun);
            this.DeleteCommand = new DelegateCommand(DeleteFun, CanExecuteNeedRecordFun);
            this.ReFreshCommand = new DelegateCommand(ReFreshFun, CanUseCommand);
            this.PrintReportCommand = new DelegateCommand(PrintReportFun, CanUseCommand);
        }


        protected virtual void OnEntitiesAssigned(Func<TEntity> getSelectedEntityCallback)
        {
            var selectedItem = getSelectedEntityCallback();
            if (selectedItem != null)
            {
                this.SelEntities.Clear();
                this.SelEntities.Add(selectedItem);
                this.SelEntity = selectedItem;
            }
            
        }

        /// <summary>
        /// 获取选择行回调
        /// </summary>
        /// <returns></returns>
        protected virtual Func<TEntity> GetSelectedEntityCallback()
        {
            
            var selectedItemId = this.SelEntity != null ? this.SelEntity.Id : default(TPrimaryKey);
            return () => {
                if (this.entities == null)
                    return null;
                return this.entities.SingleOrDefault(x => EqualityComparer<TPrimaryKey>.Default.Equals(x.Id, selectedItemId));
            }; 
        }

        /// <summary>
        /// 传参数处理
        /// </summary>
        /// <param name="parameter"></param>
        protected override void OnParameterChanged(object parameter)
        {
            var menuDescription = parameter as MenuDescription;
            if (menuDescription != null)
            {
                DocDescription docDesc = new DocDescription();
                base.Icon = menuDescription.Icon;
                base.DocTitle = menuDescription.MenuTitle;
            }
            base.OnParameterChanged(parameter);
        }

        #region 服务

        /// <summary>
        /// 报表管理服务
        /// </summary>
        protected IMyGridReportManagerService GridReportManagerService
        {
            get
            {
                return this.GetService<IMyGridReportManagerService>();
            }
        }

        #endregion

        #region 命令

        #region 检查

        /// <summary>
        /// 命令启用检查
        /// </summary>
        /// <returns></returns>
        protected virtual bool CanUseCommand()
        {
            if (base.IsLoading == true)
                return false;
            return true;
        }


        /// <summary>
        /// 检查需要数据操作
        /// </summary>
        /// <returns></returns>
        protected virtual bool CanExecuteNeedRecordFun()
        {
            return CanUseCommand() && this.SelEntities != null && this.SelEntities.Count > 0;
        }

        #endregion

        #region 创建

        /// <summary>
        /// 创建命令
        /// </summary>
        public DelegateCommand CreateCommand { get; set; }

        /// <summary>
        /// 创建
        /// </summary>
        private void CreateFun()
        {
            OnCreate();
        }

        /// <summary>
        /// 获取创建文档参数
        /// </summary>
        protected virtual CreateDocParam GetCreateDocParam()
        {
            return null;
        }

        /// <summary>
        /// 创建时
        /// </summary>
        protected virtual void OnCreate()
        {
            CreateDocParam param = GetCreateDocParam();
            if (param == null)
                return;

            DocFormParam formParame = new DocFormParam();
            formParame.FormStatus = FormStatus.Create;

            var document = this.DocumentManagerService.CreateDocument(param.DocumentType, formParame, this);
            if (document != null)
            {
                document.DestroyOnClose = true;
                document.Show();
            }
        }

        #endregion

        #region 编辑

        /// <summary>
        /// 获取编辑文档参数
        /// </summary>
        protected virtual CreateDocParam GetEditDocParam()
        {
            return null;
        }

        /// <summary>
        /// 编辑命令
        /// </summary>
        public DelegateCommand EditCommand { get; set; }

        /// <summary>
        /// 编辑
        /// </summary>
        private void EditFun()
        {
            OnEdit(this.SelEntity);
        }

        /// <summary>
        /// 编辑时
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void OnEdit(TEntity entity)
        {
            CreateDocParam param = GetEditDocParam();
            if (param == null)
                return;

            DocFormParam formParame = new DocFormParam();
            formParame.FormStatus = FormStatus.Edit;
            formParame.Parame = param.Parameters;

            var document = this.DocumentManagerService.CreateDocument(param.DocumentType, formParame, this);
            if (document != null)
            {
                document.DestroyOnClose = true;
                document.Show();
            }

        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除命令
        /// </summary>
        public DelegateCommand DeleteCommand { get; set; }

        /// <summary>
        /// 删除
        /// </summary>
        private void DeleteFun()
        {
            // TODO: 异步线程删除不刷新问题
            OnDelete(this.SelEntities);
            SyncLoad();            
        }

        /// <summary>
        /// 删除时
        /// </summary>
        protected virtual void OnDelete(IList<TEntity> entitys)
        {
           
        }

        #endregion

        #region 刷新

        /// <summary>
        /// 刷新命令
        /// </summary>
        public DelegateCommand ReFreshCommand { get; set; }

        /// <summary>
        /// 刷新
        /// </summary>
        private void ReFreshFun()
        {
            OnRefresh();
        }

        /// <summary>
        /// 刷新时
        /// </summary>
        protected virtual void OnRefresh()
        {
            SyncLoad();
        } 
        #endregion

        #region 打印报表

        /// <summary>
        /// 打印报表 命令
        /// </summary>
        public DelegateCommand PrintReportCommand { get; set; }

        /// <summary>
        /// 打印报表
        /// </summary>
        private void PrintReportFun()
        {
            OnPrintReport();
        }

        /// <summary>
        /// 打印报表时
        /// </summary>
        protected virtual void OnPrintReport()
        {
            if (this.GridReportManagerService == null)
                return;
            this.GridReportManagerService.ShowReportPreviewDialog();
        }

        #endregion

        #endregion

        #region 属性

        /// <summary>
        /// 列表数据源
        /// </summary>
        public ObservableCollection<TEntity> Entities
        {
            get
            {
                if (this.entities == null && !base.IsLoading)
                    SyncLoad();
                return this.entities;
            }
        }

        private TEntity _SelEntity;

        /// <summary>
        /// 选择行
        /// </summary>
        public TEntity SelEntity
        {
            get
            {
                return this._SelEntity;
            }
            set
            {
                SetProperty(ref this._SelEntity, value, () => this.SelEntity);
            }
        }

        private ObservableCollection<TEntity> _SelEntities = new ObservableCollection<TEntity>();

        /// <summary>
        /// 选中行集合
        /// </summary>
        /// <remarks>
        /// 坑爹有BUG，集合需要手动写
        /// </remarks>
        public ObservableCollection<TEntity> SelEntities
        {
            get
            {
                return this._SelEntities;
            }
        }

        /// <summary>
        /// 允许创建
        /// </summary>
        public bool AllowCreate
        {
            get
            {
                return GetProperty(() => this.AllowCreate);
            }
            set
            {
                SetProperty(() => this.AllowCreate, value);
            }
        }

        /// <summary>
        /// 允许编辑
        /// </summary>
        public bool AllowEdit
        {
            get
            {
                return GetProperty(() => this.AllowEdit);
            }
            set
            {
                SetProperty(() => this.AllowEdit, value);
            }
        }

        /// <summary>
        /// 允许删除
        /// </summary>
        public bool AllowDelete
        {
            get
            {
                return GetProperty(() => this.AllowDelete);
            }
            set
            {
                SetProperty(() => this.AllowDelete, value);
            }
        }
        #endregion
    }
}
