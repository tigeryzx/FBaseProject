using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IFoxtec.Common.WPF.ViewModels
{
    public interface IBaseListViewModel : IBaseViewModel
    {
        /// <summary>
        /// 异步加载列表数据
        /// </summary>
        /// <returns></returns>
        CancellationTokenSource SyncLoad();
    }
}
