﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.Common.WPF.BaseModel
{
    public class BaseEntity<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
    }
}
