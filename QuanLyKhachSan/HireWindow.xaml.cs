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
using QuanLyKhachSan.ViewModel;

namespace QuanLyKhachSan
{
    /// <summary>
    /// Interaction logic for HireWindow.xaml
    /// </summary>
    public partial class HireWindow : Window
    {
        public HireViewModel hireViewModel { get; set; }
        public HireWindow()
        {
            InitializeComponent();
            this.DataContext = hireViewModel = new HireViewModel();
        }
    }
}
