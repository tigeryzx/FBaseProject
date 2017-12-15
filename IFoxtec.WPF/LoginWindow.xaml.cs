using IFoxtec.WPF.Module.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IFoxtec.WPF
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        LoginViewModel vm;

        public LoginWindow()
        {
            InitializeComponent();

            vm = LoginViewModel.Create();
            vm.GoToMainWin = GoToMainWin;
            this.DataContext = vm;
        }

        /// <summary>
        /// 跳转到主界面
        /// </summary>
        public void GoToMainWin()
        {
            this.Hide();
            IFoxtec.WPF.MainWindow dr = new MainWindow();
            dr.Show();
            Application.Current.MainWindow = dr;
            this.Close();
        }
    }
}
