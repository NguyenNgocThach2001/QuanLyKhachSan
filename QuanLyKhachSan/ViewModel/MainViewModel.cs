using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using QuanLyKhachSan.Model;
using System.Windows.Media;

namespace QuanLyKhachSan.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region commands
        public ICommand window_IsLoaded { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand SNameChangedCommand { get; set; }
        public ICommand STypeChangedCommand { get; set; }
        public ICommand SStatusChangedCommand { get; set; }
        public ICommand SFromAChangedCommand { get; set; }
        public ICommand SToBChangedCommand { get; set; }
        public ICommand ManagementCommand { get; set; }

        #endregion
        private ObservableCollection<RoomModel> _RoomList;
        public ObservableCollection<RoomModel> RoomList
        {
            get => _RoomList;
            set
            {
                _RoomList = value; OnPropertyChanged();
            }
        }
        
        public static bool isLoaded = false;
        private string _SName;
        public string SName
        {
            get => _SName;
            set
            {
                _SName = value; OnPropertyChanged();
            }
        }
        private string _SType;
        public string SType
        {
            get => _SType;
            set
            {
                _SType = value;
                OnPropertyChanged();
            }
        }
        private string _SStatus;
        public string SStatus
        {
            get => _SStatus;
            set
            {
                _SStatus = value;
                OnPropertyChanged();
            }
        }

        private string _SFromA;
        public string SFromA
        {
            get => _SFromA;
            set
            {
                _SFromA = value;
                OnPropertyChanged();
            }
        }

        private string _SToB;
        public string SToB
        {
            get => _SToB;
            set
            {
                _SToB = value;
                OnPropertyChanged();
            }
        }
        #region commands
        public ICommand AccountCommand { get; set; }
        public ICommand CheckoutCommand { get; set; }
        public ICommand ReportCommand { get; set; }
        public ICommand HireCommand { get; set; }
        #endregion
        public MainViewModel()
        {
            window_IsLoaded = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                if (p != null && !isLoaded)
                {
                    isLoaded = true;
                    if (p == null) return;
                    p.Hide();
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.ShowDialog();
                    var loginVM = loginWindow.DataContext as LoginViewModel;
                    if (loginVM == null) return;
                    if (loginVM.IsLogin)
                    {
                        p.Show();
                        SName = "";
                        SType = "Tất Cả";
                        SStatus = "Tất Cả";
                        SFromA = "250.000 vnđ";
                        SToB = "Không Giới Hạn";
                        LoadRoomList();
                    }
                    else
                    {
                        p.Close();
                    }
                }
            }
            );
            SearchCommand = new RelayCommand<Button>((p) => { return p == null ? false : true; }, (p) =>
            {
                LoadRoomList();
            });
            ManagementCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                QuanLyDuLieu quanlydulieu = new QuanLyDuLieu();
                quanlydulieu.ShowDialog();
                LoadRoomList();
            });
            SNameChangedCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) => { SName = p.Text; });
            STypeChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) => { SType = p.Text; });
            SStatusChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) => { SStatus = p.Text; });
            SFromAChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) => { SFromA = p.Text; });
            SToBChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) => { SToB = p.Text; });
            HireCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                HireWindow hireWindow = new HireWindow();
                hireWindow.ShowDialog();
                LoadRoomList();

            });
            ReportCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                ReportWindow reportWindow = new ReportWindow();
                reportWindow.ShowDialog();
                reportWindow.Close();
                LoadRoomList();
            });
            AccountCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                AccountWindow accountWindow = new AccountWindow();
                accountWindow.ShowDialog();
                LoadRoomList();
            });
            CheckoutCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                CheckoutWindow checkoutWindow = new CheckoutWindow();
                checkoutWindow.ShowDialog();
                LoadRoomList();
            });
        }
        void LoadRoomList()
        {
            RoomList = new ObservableCollection<RoomModel>();
            var roomList = DataProvider.Ins.db.Rooms;
            foreach (var item in roomList)
            {
                try
                {
                    var queryRoomType = DataProvider.Ins.db.RoomTypes.FirstOrDefault(x => x.room_type_id == item.room_type_id);
                    RoomModel newRoom = new RoomModel();
                    newRoom.room_name = item.room_name;
                    if (queryRoomType == null) return;
                    newRoom.room_price = VN(queryRoomType.price.ToString());
                    newRoom.room_type = queryRoomType.room_type_name;
                    newRoom.room_status = DataProvider.Ins.db.RoomStatus.FirstOrDefault(x => x.room_status_id == item.room_status_id).room_status_name;
                    if (newRoom.room_status == "Trống")
                    {
                        newRoom.color = new SolidColorBrush(Colors.Green);
                        newRoom.status = "Thuê Phòng";
                    }
                    else
                    {
                        newRoom.color = new SolidColorBrush(Colors.Red);
                        newRoom.status = "Bận";
                        if (newRoom.room_status == "Đang Thuê")
                            newRoom.status = "Thanh Toán";
                    }
                    if (SName != "")
                        if (newRoom.room_name != SName) continue;
                    if (SType != "Tất Cả")
                        if (newRoom.room_type != SType) continue;
                    if (SStatus != "Tất Cả")
                        if (newRoom.room_status != SStatus) continue;
                    int price = getVN(newRoom.room_price);
                    int priceA = getVN(SFromA);
                    int priceB = getVN(SToB);
                    if (priceA != -1)
                        if (price < priceA) continue;
                    if (priceB != -1)
                        if (price > priceB) continue;
                    RoomList.Add(newRoom);
                }
                catch {
                }
            }
            //
        }

        int getVN(string p)
        {
            if (p == "Không Giới Hạn") return -1;
            string res = "";
            bool check = false;
            for(int i = p.Length - 1; i >= 0; i--)
            {
                if (check && p[i] !='.')
                {
                    res = p[i] + res;
                }
                if (p[i] == 'v')
                {
                    check = true;
                }
            }
            return Convert.ToInt32(res);
        }

        string VN(string p)
        {
            string res = "";
            int cnt = 0;
            for(int i = p.Length - 1; i >= 1; i--)
            {
                cnt++;
                res = p[i] + res;
                if (cnt == 3)
                {
                    res = "." + res;
                    cnt = 0;
                }
            }
            res = p[0] + res + " vnđ";
            return res;
        }
    }
}
