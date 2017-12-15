using IFoxtec.WPF.Module.Account;
using System.Windows;

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
