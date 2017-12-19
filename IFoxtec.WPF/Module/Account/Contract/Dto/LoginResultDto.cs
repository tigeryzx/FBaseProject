using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.WPF.Module.Account
{
    public class LoginResultDto
    {
        public bool Success { get; set; }

        public string ErrorMessage { get; set; }

        public string ErrorDetails { get; set; } 
    }
}
