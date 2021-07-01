using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
using System.Collections.ObjectModel;
using QuanLyKhachSan.Model;

namespace QuanLyKhachSan.ViewModel
{
    public class ServiceManagementUCViewmodel:BaseViewModel
    {
        public ICommand window_IsLoaded { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateCommand { get; set; }

        private ObservableCollection<ServiceModel> _serviceList { get; set; }
        public ObservableCollection<ServiceModel> ServiceList
        {
            get => _serviceList;
            set
            {
                _serviceList = value;
                OnPropertyChanged();
            }
        }

        private string _serviceNameIP { get; set; }
        public string serviceNameIP
        {
            get => _serviceNameIP;
            set
            {
                _serviceNameIP = value;
                OnPropertyChanged();
            }
        }

        private int _unitPriceIP { get; set; }
        public int unitPriceIP
        {
            get => _unitPriceIP;
            set
            {
                _unitPriceIP = value;
                OnPropertyChanged();
            }
        }

        private string _unitIP { get; set; }
        public string unitIP
        {
            get => _unitIP;
            set
            {
                _unitIP = value;
                OnPropertyChanged();
            }
        }

        public ServiceManagementUCViewmodel()
        {
            window_IsLoaded = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {
                LoadData();
            });
            UpdateCommand = new RelayCommand<Button>((p) => { return true; }, (p) => {
                UpdateData(p);
            });
            DeleteCommand = new RelayCommand<Button>((p) => { return true; }, (p) => {
                DeleteData(p);
            });
            AddCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) => {
                AddData(p);
            });
        }

        void AddData(UserControl p)
        {
            if (serviceNameIP == "") return;
            var db = DataProvider.Ins.db;
            Service add = new Service();
            add.Service_Name = serviceNameIP;
            add.unit = unitIP;
            add.unitPrice = unitPriceIP;
            db.Services.Add(add);
            db.SaveChanges();
            LoadData();
        }

        void DeleteData(Button p)
        {
            Lib trash = new Lib();
            FrameworkElement window = trash.GetWindowParent(p);
            var w = trash.FindVisualChildByName<DataGrid>(window, "dataGrid");
            if (w != null && (ServiceModel)w.SelectedItem != null)
            {
                var db = DataProvider.Ins.db;
                Service del = new Service();
                del = db.Services.Where
                            (x => x.Service_ID ==
                            ((ServiceModel)w.SelectedItem).serviceID).First();
                db.Services.Remove(del);
                db.SaveChanges();
                LoadData();
            }
        }

        void UpdateData(Button p)
        {

        }
        
        void LoadData()
        {
            ServiceList = new ObservableCollection<ServiceModel>();
            var servicelist = DataProvider.Ins.db.Services.Where(x => x.Service_Name != "").ToList();
            foreach (var service in servicelist)
            {
                try
                {
                    ServiceModel tmp = new ServiceModel();
                    tmp.serviceID = service.Service_ID;
                    tmp.serviceName = service.Service_Name;
                    tmp.unit = service.unit;
                    tmp.unitPrice = service.unitPrice.Value;
                    ServiceList.Add(tmp);
                }
                catch { }
            }
        }
    }
}

