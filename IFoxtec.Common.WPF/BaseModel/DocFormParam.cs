using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.Common.WPF.BaseModel
{
    /// <summary>
    /// 表单文档 参数描述
    /// </summary>
    public class DocFormParam
    {
        /// <summary>
        /// 参数
        /// </summary>
        public object Parame { get; set; }

        /// <summary>
        /// 表单状态
        /// </summary>
        public FormStatus FormStatus { get; set; }
    }

    /// <summary>
    /// 表单状态
    /// </summary>
    public enum FormStatus
    {
        Create,
        Edit
    }
}
