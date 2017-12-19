using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.WPF.Common.ViewModels.PartVm
{
    public class BaseEditGridViewModel<TEntity>
        : ViewModelBase, ISupportValidate, IBaseViewModel
        where TEntity:new()
    {
        public BaseEditGridViewModel()
            :this(null)
        {

        }

        public BaseEditGridViewModel(List<TEntity> sourceEntities)
        {
            InitCommands();
        }

        /// <summary>
        /// 初始化命令
        /// </summary>
        private void InitCommands()
        {
            this.DeleteCommand = new DelegateCommand(DeleteFun, CanUseNeedEntityCommand);
        }

        private Action<TEntity> _DefaultValueCallback;

        /// <summary>
        /// 设置默认值回调
        /// </summary>
        public void SetDefaultValueCallback(Action<TEntity> callback)
        {
            this._DefaultValueCallback = callback;
        }

        #region 命令

        #region 检查

        /// <summary>
        /// 允许使用需要实体的命令
        /// </summary>
        /// <returns></returns>
        protected bool CanUseNeedEntityCommand()
        {
            return this.SelEntity != null;
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除命令
        /// </summary>
        public DelegateCommand DeleteCommand { get; set; }

        private void DeleteFun()
        {
            OnDelete(this.SelEntity);
        }

        protected virtual void OnDelete(TEntity tEntity)
        {
            this.Entities.Remove(tEntity);
        } 
        #endregion

        #endregion

        #region 属性

        /// <summary>
        /// 原始列表
        /// </summary>
        private List<TEntity> _SourceEntities;

        /// <summary>
        /// 设置原始实体列表
        /// </summary>
        /// <param name="sourceEntities">原始列表</param>
        public void SetSourceEntities(List<TEntity> sourceEntities)
        {
            this._SourceEntities = sourceEntities;
            this.Entities = new ObservableCollection<TEntity>(sourceEntities);
            this._Entities.CollectionChanged += _Entities_CollectionChanged;
        }

        private ObservableCollection<TEntity> _Entities;

        /// <summary>
        /// 数据源组
        /// </summary>
        public ObservableCollection<TEntity> Entities
        {
            get
            {
                if (this._Entities == null)
                {
                    this._Entities = new ObservableCollection<TEntity>();
                    this._Entities.CollectionChanged += _Entities_CollectionChanged;
                }
                return this._Entities;
            }
           private set
            {
                this._Entities = value;
                RaisePropertyChanged(() => this.Entities);
            }
        }

        void _Entities_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                if (this._DefaultValueCallback != null)
                {
                    var objList = ((object[])e.NewItems.SyncRoot);
                    foreach (var obj in objList)
                        this._DefaultValueCallback((TEntity)obj);
                }
                    
            }

            this._SourceEntities.Clear();
            foreach (var item in this.Entities)
                this._SourceEntities.Add(item);
        }

   
        /// <summary>
        /// 选择实体
        /// </summary>
        public TEntity SelEntity
        {
            get
            {
                return GetProperty(() => this.SelEntity);
            }
            set
            {
                SetProperty(() => this.SelEntity, value);
            }
        }

        /// <summary>
        /// 是否存在验证错误
        /// </summary>
        public bool HasValidationError
        {
            get
            {
                return GetProperty(() => this.HasValidationError);
            }
            set
            {
                SetProperty(() => this.HasValidationError, value);
            }
        }

        #endregion

        #region 表格验证

        /// <summary>
        /// 表格验证
        /// </summary>
        /// <returns>True 成功</returns>
        public bool Validate()
        {
            if (this.Entities != null)
            {
                foreach (var item in this.Entities)
                {
                    ICollection<ValidationResult> validationResults = new List<ValidationResult>();
                    ValidationContext validationContext = new ValidationContext(item);
                    if (!Validator.TryValidateObject(item, validationContext, validationResults))
                        return false;
                }
                return true;
            }
            else
                return true;
        }

        #endregion
    }
}
