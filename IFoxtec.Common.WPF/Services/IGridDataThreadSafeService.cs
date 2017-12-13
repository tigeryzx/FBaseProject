using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.Common.WPF.Services
{
    /// <summary>
    /// Grid数据线程安全服务 接口
    /// </summary>
    public interface IGridDataThreadSafeService
    {
        /// <summary>
        /// 开始更新
        /// </summary>
        void BeginUpdate();

        /// <summary>
        /// 结束更新
        /// </summary>
        void EndUpdate();
    }
}
