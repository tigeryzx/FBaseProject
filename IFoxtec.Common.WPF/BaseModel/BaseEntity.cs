using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.WPF.Common.BaseModel
{
    public class BaseEntity<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
    }
}
