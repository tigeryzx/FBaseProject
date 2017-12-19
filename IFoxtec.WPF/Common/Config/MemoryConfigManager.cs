using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.WPF.Common.Config
{
    /// <summary>
    /// 内存配置管理器
    /// </summary>
    public class MemoryConfigManager : IConfigManager
    {
        private Dictionary<string, object> _Dictionary = new Dictionary<string, object>();

        public TResult Get<TResult>(string key)
        {
            object result;
            if (this._Dictionary.TryGetValue(key, out result))
                return (TResult)result;
            else
                return default(TResult);
        }

        public void Remove(string key)
        {
            if (this._Dictionary.ContainsKey(key))
                this._Dictionary.Remove(key);
        }

        public void Set(string key, object value)
        {
            if (this._Dictionary.ContainsKey(key))
                this._Dictionary[key] = value;
            else
                this._Dictionary.Add(key, value);
        }
    }
}
