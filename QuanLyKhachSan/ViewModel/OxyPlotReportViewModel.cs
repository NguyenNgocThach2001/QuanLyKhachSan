using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using OxyPlot;
using OxyPlot.Series;
using QuanLyKhachSan.Model;

namespace QuanLyKhachSan.ViewModel
{

    public class OxyPlotReportViewModel : BaseViewModel
    {
        public ObservableCollection<OxyPlotReportModel> MyModels = new ObservableCollection<OxyPlotReportModel>();
        public OxyPlotReportViewModel()
        {
            
        }
    }
}
