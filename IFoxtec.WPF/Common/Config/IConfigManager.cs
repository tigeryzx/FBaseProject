using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.WPF.Common.Config
{
    /// <summary>
    /// 配置管理器
    /// </summary>
    public interface IConfigManager
    {
        TResult Get<TResult>(string key);

        void Set(string key, object value);

        void Remove(string key);
      
    }
}
