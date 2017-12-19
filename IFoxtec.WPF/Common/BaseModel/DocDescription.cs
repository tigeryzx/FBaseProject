using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace IFoxtec.WPF.Common.BaseModel
{
    /// <summary>
    /// 文档描述
    /// </summary>
    public class DocDescription
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get
            {
                if (!string.IsNullOrEmpty(this.DocumentNameExt))
                    return string.Format("{0}[1]", this.DocumentName, this.DocumentNameExt);
                return this.DocumentName;
            }
        }

        /// <summary>
        /// 文档扩展名称
        /// </summary>
        public string DocumentNameExt
        {
            get;
            set;
        }

        /// <summary>
        /// 文档名称
        /// </summary>
        public string DocumentName
        {
            get;
            set;
        }

        /// <summary>
        /// 文档图标
        /// </summary>
        public ImageSource Icon
        {
            get;
            set;
        } 
    }
}
