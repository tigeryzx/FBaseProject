using IFoxtec.WPF.Module.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.WPF.Module
{
    public class PageViewIndex
    {
        public static string UserListView { get { return typeof(UserListView).FullName; } }

        public static string CreateUserView { get { return typeof(CreateUserView).FullName; } }
    }
}
