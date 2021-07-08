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
    public class ReportViewModel:BaseViewModel
    {
        private PlotModel _MyModel = new PlotModel();
        public PlotModel MyModel
        {
            get => _MyModel;
            set
            {
                _MyModel = value;
                OnPropertyChanged();
            }
        }

        public ReportViewModel()
        {
            MyModel.Title = "Báo cáo ngày";
            MyModel.PlotType = PlotType.XY;
        }
    }
}
