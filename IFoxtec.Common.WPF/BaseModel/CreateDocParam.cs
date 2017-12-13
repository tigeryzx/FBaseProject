using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.Common.WPF.BaseModel
{
    /// <summary>
    /// 创建文档参数
    /// </summary>
    public class CreateDocParam
    {
        /// <summary>
        /// 文档类型
        /// </summary>
        public string DocumentType { get; set; }

        /// <summary>
        /// 文档标题
        /// </summary>
        public string DocumentTitle { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public object Parameters { get; set; }
    }
}
