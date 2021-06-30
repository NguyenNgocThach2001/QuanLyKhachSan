using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using QuanLyKhachSan.Model;

namespace QuanLyKhachSan.ViewModel
{
    public class RoomManagementUCViewModel:BaseViewModel
    {
        private string _RoomNameIP { get; set; }
        public string RoomNameIP
        {
            get => _RoomNameIP;
            set
            {
                _RoomNameIP = value;
                OnPropertyChanged();
            }
        }
        public ICommand window_IsLoaded { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        private ObservableCollection<RoomMgModel> _RoomList = new ObservableCollection<RoomMgModel>();
        public ObservableCollection<RoomMgModel> RoomList
        {
            get => _RoomList;
            set
            {
                _RoomList = value;
                OnPropertyChanged();
            }
        }

        public RoomManagementUCViewModel()
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


        void LoadData()
        {
            RoomList = new ObservableCollection<RoomMgModel>();
            var roomlist = DataProvider.Ins.db.Rooms.Where(x => x.room_name != "").ToList();
            foreach (var room in roomlist)
            {
                try
                {
                    RoomMgModel tmp = new RoomMgModel();
                    tmp.RoomId = room.room_id;
                    tmp.RoomName = room.room_name;
                    tmp.RoomStatusId = room.room_status_id.Value;
                    tmp.RoomTypeId = room.room_type_id.Value;
                    RoomList.Add(tmp);
                }
                catch { }
            }
        }

        void AddData(UserControl p)
        {
            if (RoomNameIP == "") return;
            var db = DataProvider.Ins.db;
            Room add = new Room();
            add.room_name = RoomNameIP;
            //add.room_type_id = RoomTypeIDIP;
            //add.room_status_id = RoomStatusIDIP;
            db.Rooms.Add(add);
            db.SaveChanges();
            LoadData();
        }

        void DeleteData(Button p)
        {
            Lib trash = new Lib();
            FrameworkElement window = trash.GetWindowParent(p);
            var w = trash.FindVisualChildByName<DataGrid>(window, "dataGrid");
            if (w != null && (RoomMgModel)w.SelectedItem != null)
            {
                var db = DataProvider.Ins.db;
                Room del = new Room();
                del = db.Rooms.Where
                            (x => x.room_id ==
                            ((RoomMgModel)w.SelectedItem).RoomId).First();
                db.Rooms.Remove(del);
                db.SaveChanges();
                LoadData();
            }
        }

        void UpdateData(Button p)
        {

        }
    }
}

