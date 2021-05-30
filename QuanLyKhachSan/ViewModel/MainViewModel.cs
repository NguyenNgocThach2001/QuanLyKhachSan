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
        public MainViewModel()
        {
            window_IsLoaded = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                if (p != null && !isLoaded)
                {
                    isLoaded = true;
                    LoginWindow loginWindow = new LoginWindow();
                    p.Hide();
                    loginWindow.ShowDialog();
                    var loginVM = loginWindow.DataContext as LoginViewModel;
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
        }

    }
}
