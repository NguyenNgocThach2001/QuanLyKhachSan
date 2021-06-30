using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using QuanLyKhachSan.Model;

namespace QuanLyKhachSan.ViewModel
{
    public class RoomTypeManagementUCViewmodel : BaseViewModel
    {

        private string _RoomTypeNameIP { get; set; }
        public string RoomTypeNameIP
        {
            get => _RoomTypeNameIP;
            set
            {
                _RoomTypeNameIP = value;
                OnPropertyChanged();
            }
        }
        private int _RoomPriceIP { get; set; }
        public int RoomPriceIP
        {
            get => _RoomPriceIP;
            set
            {
                _RoomPriceIP = value;
                OnPropertyChanged();
            }
        }
        public ICommand window_IsLoaded { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
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

        void init()
        {
            RoomPriceIP = 0;
        }
        public RoomTypeManagementUCViewmodel()
        {
            init();
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
            RoomTypeList = new ObservableCollection<RoomTypeModel>();
            var roomtypelist = DataProvider.Ins.db.RoomTypes.Where(x => x.room_type_name != "").ToList();
            foreach (var roomtype in roomtypelist)
            {
                try
                {
                    RoomTypeModel tmp = new RoomTypeModel();
                    tmp.RoomTypeId = roomtype.room_type_id;
                    tmp.RoomTypeName = roomtype.room_type_name;
                    tmp.Price = roomtype.price.Value;
                    RoomTypeList.Add(tmp);
                }
                catch { }
            }
        }

        void AddData(UserControl p)
        {
            if (RoomTypeNameIP == "") return;
            var db = DataProvider.Ins.db;
            RoomType add = new RoomType();
            add.room_type_name = RoomTypeNameIP;
            add.price = RoomPriceIP;
            db.RoomTypes.Add(add);
            db.SaveChanges();
            LoadData();
        }

        void DeleteData(Button p)
        {
            Lib trash = new Lib();
            FrameworkElement window = trash.GetWindowParent(p);
            var w = trash.FindVisualChildByName<DataGrid>(window, "dataGrid");
            if (w != null && (RoomTypeModel)w.SelectedItem != null)
            {
                var db = DataProvider.Ins.db;
                RoomType del = new RoomType();
                del = db.RoomTypes.Where
                            (x => x.room_type_id ==
                            ((RoomTypeModel)w.SelectedItem).RoomTypeId).First();
                db.RoomTypes.Remove(del);
                db.SaveChanges();
                LoadData();
            }
        }

        void UpdateData(Button p)
        {

        }
    }
}
