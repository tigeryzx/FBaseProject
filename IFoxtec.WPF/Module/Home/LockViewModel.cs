using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;

namespace IFoxtec.WPF.Module.Home
{
    //[POCOViewModel(ImplementIDataErrorInfo = true)]
    public class LockViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        public LockViewModel()
        {
            this.Password = string.Empty;
        }

        private string _Password;

        /// <summary>
        /// 锁屏密码
        /// </summary>
        //[Required]
        public string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                SetProperty(ref this._Password, value, () => this.Password);
                ValidationPassword(value);
            }
        }

        /// <summary>
        /// 是否通过
        /// </summary>
        public bool IsPass { get; set; }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private void RaiseErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private Dictionary<string, List<string>> _Dictionary = new Dictionary<string, List<string>>();

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (this._Dictionary.ContainsKey(propertyName))
                return this._Dictionary[propertyName];
            return null;
        }

        private void SetError(string propertyName, List<string> errors)
        {
            if (this._Dictionary.ContainsKey(propertyName))
                this._Dictionary.Remove(propertyName);
            if (errors != null && errors.Count > 0)
                this._Dictionary.Add(propertyName, errors);
            RaiseErrorsChanged(propertyName);
        }

        public bool HasErrors
        {
            get { return this._Dictionary.Count > 0; }
        }

        /// <summary>
        /// 验证密码
        /// </summary>
        /// <returns></returns>
        private void ValidationPassword(string password)
        {
            List<string> errors = new List<string>();

            if (password != "123456")
                errors.Add("密码不匹配");

            SetError("Password", errors);
            this.IsPass = errors.Count <= 0;
        }
    }
}