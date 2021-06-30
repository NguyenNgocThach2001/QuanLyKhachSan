using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using QuanLyKhachSan.Model;
using System.Windows.Media;

namespace QuanLyKhachSan.ViewModel
{
    public class RoomStatusManagementUCViewmodel:BaseViewModel
    {
        private string _RoomStatusNameIP { get; set; }
        public string RoomStatusNameIP
        {
            get => _RoomStatusNameIP;
            set
            {
                _RoomStatusNameIP = value;
                OnPropertyChanged();
            }
        }
        public ICommand window_IsLoaded { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        private ObservableCollection<RoomStatusModel> _RoomStatusList = new ObservableCollection<RoomStatusModel>();
        public ObservableCollection<RoomStatusModel> RoomStatusList
        {
            get => _RoomStatusList;
            set
            {
                _RoomStatusList = value;
                OnPropertyChanged();
            }
        }
        public RoomStatusManagementUCViewmodel()
        {
            window_IsLoaded = new RelayCommand<UserControl>((p) => { return true; }, (p) => {
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
        void DeleteData(Button p)
        {
            Lib trash = new Lib();
            FrameworkElement window = trash.GetWindowParent(p);
            var w = trash.FindVisualChildByName<DataGrid>(window, "dataGrid");
            if (w != null && (RoomStatusModel)w.SelectedItem != null)
            {
                var db = DataProvider.Ins.db;
                RoomStatu del = new RoomStatu();
                del = db.RoomStatus.Where
                            (x => x.room_status_id ==
                            ((RoomStatusModel)w.SelectedItem).RoomStatusId).First();
                db.RoomStatus.Remove(del);
                db.SaveChanges();
                LoadData();
            }
        }

        void AddData(UserControl p)
        {
            if (RoomStatusNameIP == "") return;
            var db = DataProvider.Ins.db;
            RoomStatu add = new RoomStatu();
            add.room_status_name = RoomStatusNameIP;
            db.RoomStatus.Add(add);
            db.SaveChanges();
            LoadData();
        }
        void UpdateData(Button p)
        {

        }
        void LoadData()
        {
            RoomStatusList = new ObservableCollection<RoomStatusModel>();
            var roomtypelist = DataProvider.Ins.db.RoomStatus.Where(x=>x.room_status_name!="").ToList();
            foreach(var roomtype in roomtypelist)
            {
                RoomStatusModel tmp = new RoomStatusModel();
                tmp.RoomStatusId = roomtype.room_status_id;
                tmp.RoomStatusName = roomtype.room_status_name;
                RoomStatusList.Add(tmp);
            }
        }
    }
}
