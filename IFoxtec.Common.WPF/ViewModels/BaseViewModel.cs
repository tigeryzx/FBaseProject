using Abp;
using Abp.Dependency;
using Abp.Localization;
using Abp.Localization.Sources;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.Common.WPF.ViewModels
{
    public class BaseViewModel : ViewModelBase, IBaseViewModel, ITransientDependency
    {
        public ILocalizationManager LocalizationManager
        {
            protected get;
            set;
        }

        protected string LocalizationSourceName
        {
            get;
            set;
        }

        private ILocalizationSource _localizationSource;


        protected ILocalizationSource LocalizationSource
        {
            get
            {
                if (this.LocalizationSourceName == null)
                {
                    throw new AbpException("Must set LocalizationSourceName before, in order to get LocalizationSource");
                }
                if (this._localizationSource == null || this._localizationSource.Name != this.LocalizationSourceName)
                {
                    this._localizationSource = this.LocalizationManager.GetSource(this.LocalizationSourceName);
                }
                return this._localizationSource;
            }
        }


        public BaseViewModel()
        {
            // 注入语言服务
            this.LocalizationManager = NullLocalizationManager.Instance;
            LocalizationSourceName = "IFoxtec";
        }

        /// <summary>
        /// Gets localized string for given key name and current language.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <returns>Localized string</returns>
        protected virtual string L(string name)
        {
            return this.LocalizationSource.GetString(name);
        }

        /// <summary>
        /// Gets localized string for given key name and current language with formatting strings.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="args">Format arguments</param>
        /// <returns>Localized string</returns>
        protected string L(string name, params object[] args)
        {
            return this.LocalizationSource.GetString(name, args);
        }

        /// <summary>
        /// Gets localized string for given key name and specified culture information.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="culture">culture information</param>
        /// <returns>Localized string</returns>
        protected virtual string L(string name, CultureInfo culture)
        {
            return this.LocalizationSource.GetString(name, culture);
        }

        /// <summary>
        /// Gets localized string for given key name and current language with formatting strings.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="culture">culture information</param>
        /// <param name="args">Format arguments</param>
        /// <returns>Localized string</returns>
        protected string L(string name, CultureInfo culture, params object[] args)
        {
            return this.LocalizationSource.GetString(name, culture, args);
        }
    }
}
