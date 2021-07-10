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
using OxyPlot.Axes;
using OxyPlot.Series;
using QuanLyKhachSan.Model;
using System.ComponentModel;


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
            MyModel.Axes.Add(new LinearAxis());
            MyModel.Axes.Add(new LinearAxis());
            MyModel.Axes[0].Position = AxisPosition.Bottom;
            MyModel.Axes[0].Maximum = 100;
            MyModel.Axes[0].Minimum = 0;
            MyModel.Axes[1].Position = AxisPosition.Left;
            MyModel.Axes[1].Maximum = 100;
            MyModel.Axes[1].Minimum = 0;
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            (sender as ReportWindow).Hide();
            e.Cancel = true;
        }
    }
}
