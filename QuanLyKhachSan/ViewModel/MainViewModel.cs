using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace QuanLyKhachSan.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region commands
        public ICommand window_IsLoaded { get; set; }
        #endregion
        public static bool isLoaded = false;
        #region commands
        public ICommand AccountCommand { get; set; }
        public ICommand CheckoutCommand { get; set; }
        public ICommand ReportCommand { get; set; }
        public ICommand HireCommand { get; set; }
        #endregion
        public MainViewModel()
        {
            window_IsLoaded = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                if (p != null && !isLoaded)
                {
                    isLoaded = true;
                    if (p == null) return;
                    p.Hide();
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.ShowDialog();
                    var loginVM = loginWindow.DataContext as LoginViewModel;
                    if (loginVM == null) return;
                    if (loginVM.IsLogin)
                    {
                        p.Show();
                    }
                    else
                    {
                        p.Close();
                    }
                }
            }
            );
            HireCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                HireWindow hireWindow = new HireWindow();
                hireWindow.ShowDialog();
            });
            ReportCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                ReportWindow reportWindow = new ReportWindow();
                reportWindow.ShowDialog();
            });
            AccountCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                AccountWindow accountWindow = new AccountWindow();
                accountWindow.ShowDialog();
            });
            CheckoutCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                CheckoutWindow checkoutWindow = new CheckoutWindow();
                checkoutWindow.ShowDialog();
            });
        }

    }
}
