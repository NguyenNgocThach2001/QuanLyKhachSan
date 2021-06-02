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
                        LoadRoomList();
                    }
                    else
                    {
                        p.Close();
                    }
                }
            }
            );
            HireCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                HireWindow hireWindow = new HireWindow();
                hireWindow.ShowDialog();
            });
            ReportCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                ReportWindow reportWindow = new ReportWindow();
                reportWindow.ShowDialog();
            });
            AccountCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                AccountWindow accountWindow = new AccountWindow();
                accountWindow.ShowDialog();
            });
            CheckoutCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                CheckoutWindow checkoutWindow = new CheckoutWindow();
                checkoutWindow.ShowDialog();
            });
        }
        void LoadRoomList()
        {
            RoomList = new ObservableCollection<RoomModel>();
            var roomList = DataProvider.Ins.db.Rooms;
            foreach(var item in roomList)
            {
                var queryRoomType = DataProvider.Ins.db.RoomTypes.FirstOrDefault(x => x.room_type_id == item.room_type_id);
                RoomModel newRoom = new RoomModel();
                newRoom.room_name = item.room_name;
                newRoom.room_price = queryRoomType.price.ToString() + " vnđ";
                newRoom.room_type = queryRoomType.room_type_name;
                newRoom.room_status = DataProvider.Ins.db.RoomStatus.FirstOrDefault(x => x.room_status_id == item.room_status_id).room_status_name;
                if (newRoom.room_status == "Trống")
                    newRoom.color = new SolidColorBrush(Colors.Green);
                else
                {
                    newRoom.color = new SolidColorBrush(Colors.Red);
                }
                RoomList.Add(newRoom);
            }
        }
    }
}
