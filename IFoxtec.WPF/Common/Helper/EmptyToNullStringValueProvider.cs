using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.WPF.Common.Helper
{
    public class EmptyToNullStringValueProvider : IValueProvider
    {
        PropertyInfo _MemberInfo;
        public EmptyToNullStringValueProvider(PropertyInfo memberInfo)
        {
            _MemberInfo = memberInfo;
        }

        public object GetValue(object target)
        {
            object result = _MemberInfo.GetValue(target);

            if (result == null)
                return null;

            if (_MemberInfo.PropertyType == typeof(string) && result.ToString() == "")
                result = null;
            return result;
        }

        public void SetValue(object target, object value)
        {
            _MemberInfo.SetValue(target, value);
        }
    }
}
