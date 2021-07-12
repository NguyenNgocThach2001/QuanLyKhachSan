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
        public ICommand window_IsLoaded { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateCommand { get; set; }

        public ICommand RoomTypeChangedCommand { get; set; }
        public ICommand RoomStatusChangedCommand { get; set; }
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

        private ObservableCollection<RoomTypeModel> _RoomTypeList = new ObservableCollection<RoomTypeModel>();
        public ObservableCollection<RoomTypeModel> RoomTypeList
        {
            get => _RoomTypeList;
            set
            {
                _RoomTypeList = value;
                OnPropertyChanged();
            }
        }

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
        private string _SRoomStatusName = "";
        public string SRoomStatusName
        {
            get => _SRoomStatusName;
            set
            {
                _SRoomStatusName = value;
                OnPropertyChanged();
            }
        }

        private RoomStatusModel _SRoomStatus { get; set; }
        public RoomStatusModel SRoomStatus
        {
            get => _SRoomStatus;
            set
            {
                _SRoomStatus = value;
                OnPropertyChanged();
            }
        }

        private RoomTypeModel _SRoomType { get; set; }
        public RoomTypeModel SRoomType
        {
            get => _SRoomType;
            set
            {
                _SRoomType = value;
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
            RoomTypeChangedCommand = new RelayCommand<ComboBox>((p) => { return p != null; },
                                     (p) => { 
                                         try 
                                         { 
                                             SRoomType = ((RoomTypeModel)p.SelectedItem); 
                                         } 
                                         catch { }; 
                                     });
            RoomStatusChangedCommand = new RelayCommand<ComboBox>((p) => { return p != null; },
                                     (p) => { 
                                         try { 
                                             SRoomStatus = ((RoomStatusModel)p.SelectedItem); 
                                         } 
                                         catch { }; 
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
                    tmp.RoomStatusName = DataProvider.Ins.db.RoomStatus.FirstOrDefault(x => x.room_status_id == tmp.RoomStatusId).room_status_name;
                    tmp.RoomTypeName = DataProvider.Ins.db.RoomTypes.FirstOrDefault(x => x.room_type_id == tmp.RoomTypeId).room_type_name;
                    RoomList.Add(tmp);
                }
                catch { }
            }
            AddRoomStatusListToComboBox();
            AddRoomTypeListToComboBox();
        }
        void AddRoomStatusListToComboBox()
        {
            RoomStatusList.Clear();
            var roomList = DataProvider.Ins.db.RoomStatus.ToList();
            foreach (var item in roomList)
            {
                RoomStatusModel newRoom = new RoomStatusModel();
                newRoom.RoomStatusId = item.room_status_id;
                newRoom.RoomStatusName = item.room_status_name;
                RoomStatusList.Add(newRoom);
            }
        }

        void AddRoomTypeListToComboBox()
        {
            RoomTypeList.Clear();
            var roomTypeList = DataProvider.Ins.db.RoomTypes.ToList();
            foreach (var item in roomTypeList)
            {
                RoomTypeModel newRoom = new RoomTypeModel();
                newRoom.RoomTypeId = item.room_type_id;
                newRoom.RoomTypeName = item.room_type_name;
                RoomTypeList.Add(newRoom);
            }
        }
        void AddData(UserControl p)
        {
            if (RoomNameIP == "") return;
            var db = DataProvider.Ins.db;
            Room add = new Room();
            add.room_name = RoomNameIP;
            add.room_type_id = SRoomType.RoomTypeId;
            add.room_status_id = SRoomStatus.RoomStatusId;
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

