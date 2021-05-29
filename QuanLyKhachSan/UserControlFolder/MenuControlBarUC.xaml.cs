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
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuanLyKhachSan.ViewModel;

namespace QuanLyKhachSan.UserControlFolder
{
    /// <summary>
    /// Interaction logic for MenuControlBarUC.xaml
    /// </summary>
    public partial class MenuControlBarUC : UserControl
    {
        public ControlBarViewModel viewModel;
        public MenuControlBarUC()
        {
            InitializeComponent();
            this.DataContext = viewModel = new ControlBarViewModel();
        }
    }
}
