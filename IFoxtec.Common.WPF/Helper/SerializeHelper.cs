using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.WPF.Common.Helper
{
    /// <summary>
    /// 序列化 辅助类
    /// </summary>
    public class SerializeHelper
    {
        private static JsonSerializerSettings GetSerializerSetting()
        {
            var settings = new JsonSerializerSettings() { ContractResolver = new EmptyToNullStringResolver() };
            return settings;
        }

        /// <summary>
        /// 获取对象序列化字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string GetObjectSerializeString(object obj)
        {
            var json = JsonConvert.SerializeObject(obj, GetSerializerSetting());
            return Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(json));
        }

        /// <summary>
        /// 字符串转对象（Base64编码字符串反序列化为对象）
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>对象</returns>
        public static T StringToObject<T>(string str)
        {
            byte[] bytes = Convert.FromBase64String(str);
            var json = System.Text.Encoding.Default.GetString(bytes);
            return JsonConvert.DeserializeObject<T>(json, GetSerializerSetting());
        }
    }
}
