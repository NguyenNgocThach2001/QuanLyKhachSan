using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

namespace QuanLyKhachSan.ViewModel
{
    public class LoginViewModel:BaseViewModel
    {
        #region commands
        public ICommand loginDone_btnClick { get; set; }
        public ICommand loginClose_btnClick { get; set; }
        #endregion
        FrameworkElement GetWindowParent(UserControl p)
        {
            FrameworkElement parent = p;
            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }
            return parent;
        }
    }
}
