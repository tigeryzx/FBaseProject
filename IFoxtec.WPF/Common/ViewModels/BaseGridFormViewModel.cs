using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.WPF.Common.ViewModels
{
    /// <summary>
    /// 基础带Grid的表单 VM
    /// </summary>
    public class BaseGridFormViewModel<TEntity> : BaseFormViewModel<TEntity>
        where TEntity : new()
    {

    }
}
