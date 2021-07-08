using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLyKhachSan.Model;

namespace QuanLyKhachSan.ViewModel
{
    public class AddHireViewModel:BaseViewModel
    {
        #region commands
        public ICommand RoomComboBoxCommand { get; set; }
        public ICommand GuestNameChangedCommand { get; set; }
        public ICommand CMNDChangedCommand { get; set; }
        public ICommand AddHireCommand { get; set;}
        public ICommand CheckinDateCommand { get; set; }
        public ICommand CheckoutDateCommand { get; set; }
        public ICommand Window_IsLoaded { get; set; }
        public ICommand RoomNameChangedCommand { get; set; }

        #endregion
        private string _SRoomName = "";
        public string SRoomName
        {
            get => _SRoomName;
            set
            {
                _SRoomName = value;
                OnPropertyChanged();
            }
        }
        private Reservation _reservation { get; set; }
        public Reservation reservation
        {
            get => _reservation;
            set { 
                _reservation = value;
            }
        }
        private ObservableCollection<AddHireModel> _RoomNameList = new ObservableCollection<AddHireModel>();
        public ObservableCollection<AddHireModel> RoomNameList
        {
            get => _RoomNameList;
            set
            {
                _RoomNameList = value;
                OnPropertyChanged();
            }
        }

        private string _GuestName = "";
        public string GuestName { 
            get => _GuestName;
            set
            {
                _GuestName = value;
                OnPropertyChanged();
            }
        }
        private string _RoomName = "";
        public string RoomName
        {
            get => _RoomName;
            set
            {
                _RoomName = value;
                OnPropertyChanged();
            }
        }

        private string _CMND = "";
        public string CMND
        {
            get => _CMND;
            set
            {
                _CMND = value;
                OnPropertyChanged();
            }
        }

        private DateTime _CheckinDate = DateTime.Now;
        public DateTime CheckinDate
        {
            get => _CheckinDate;
            set
            {
                _CheckinDate = value;
                OnPropertyChanged();
            }
        }


        private DateTime _CheckoutDate = DateTime.Now;
        public DateTime CheckoutDate
        {
            get => _CheckoutDate;
            set
            {
                _CheckoutDate = value;
                OnPropertyChanged();
            }
        }

        private Room _SRoom { get; set; }
        public Room SRoom
        {
            get => _SRoom;
            set
            {
                _SRoom = value;
                OnPropertyChanged();
            }
        }

        public AddHireViewModel()
        {
            Window_IsLoaded = new RelayCommand<Window>((p) => { return true; },
                                                       (p) => { AddRoomListToComboBox(); });
            AddHireCommand = new RelayCommand<Button>((p) => { return p != null; }, 
                                                      (p) => AddHireToDatabase(p));
            GuestNameChangedCommand = new RelayCommand<TextBox>((p) => { return p != null; },
                                                                (p) => { GuestName = p.Text; });
            CMNDChangedCommand = new RelayCommand<TextBox>((p) => { return p != null; },
                                                           (p) => { CMND = p.Text; });
            CheckinDateCommand = new RelayCommand<DatePicker>((p) => { return p != null; },
                    (p) => { CheckinDate = DateTime.ParseExact(p.Text, "MM-dd-yyyy HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture); });

            CheckoutDateCommand = new RelayCommand<DatePicker>((p) => { return p != null; },
                    (p) => { CheckoutDate = DateTime.ParseExact(p.Text,"MM-dd-yyyy HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture); });

            RoomNameChangedCommand = new RelayCommand<ComboBox>((p) => { return p != null; },
                                     (p) => { try { SRoom = ((AddHireModel)p.SelectedItem).room; SRoomName = SRoom.room_name; } catch { }; });
        }

        void AddHireToDatabase(Button p)
        {
            if (SRoomName == "" || SRoomName == null)
            {
                MessageBox.Show("Hãy chọn phòng cần thuê");
                return;
            }
            if (GuestName == "" || GuestName == null)
            {
                MessageBox.Show("Hãy nhập tên khách hàng");
                return;
            }
            if (CMND == "" || CMND == null)
            {
                MessageBox.Show("Hãy nhập CMND");
                return;
            }
            int compareResult = DateTime.Compare(CheckinDate, CheckoutDate);
            if (compareResult >= 0)
            {
                MessageBox.Show("Ngày vào phải trước ngày ra");
                return;
            }
            reservation = new Reservation();
            reservation.check_in_date = CheckinDate;
            reservation.check_out_date = CheckoutDate;
            var guestList = DataProvider.Ins.db.Guests.ToList();
            bool checkGuestExist = false;
            Guest newGuest = new Guest();
            foreach (var item in guestList)
            {
                if (item.full_name == null || item.CMND == null) continue;
                if (item.full_name.Replace(" {2, }", " ") == GuestName.Replace(" {2, }", " ")
                    && item.CMND.Trim(' ') == CMND.Trim(' '))
                {
                    reservation.guest_id = item.guest_id;
                    checkGuestExist = true;
                    break;
                }
            }

            if (!checkGuestExist)
            {
                newGuest.CMND = CMND;
                newGuest.full_name = GuestName;
                DataProvider.Ins.db.Guests.Add(newGuest);
                DataProvider.Ins.db.SaveChanges();
                guestList = DataProvider.Ins.db.Guests.ToList();
                foreach (var item in guestList)
                {
                    if (item.full_name == null || item.CMND == null) continue;
                    if (item.full_name.Replace(" {2, }", " ") == GuestName.Replace(" {2, }", " ")
                        && item.CMND.Trim(' ') == CMND.Trim(' '))
                    {
                        reservation.guest_id = item.guest_id;
                        break;
                    }
                }
            }

            foreach (var item in RoomNameList)
            {
                if (item.room.room_name == null) continue;
                if (item.room.room_name == SRoomName)
                {
                    reservation.room_id = item.room.room_id;
                }
            }

            Room room = DataProvider.Ins.db.Rooms.FirstOrDefault(x => x.room_id == reservation.room_id);
            RoomType roomtype = DataProvider.Ins.db.RoomTypes.FirstOrDefault(x => x.room_type_id == room.room_type_id);
            RoomService roomservice = new RoomService();
            roomservice.Room_Name = roomtype.room_type_name;
            roomservice.Unit = "Ngày";
            roomservice.Unit_Price = roomtype.price;
            roomservice.Useage = (reservation.check_out_date.Value - reservation.check_in_date.Value).Days;
            roomservice.Room_Service = Guid.NewGuid();
            reservation.Room_Service = roomservice.Room_Service;
            reservation.paid = 0;
            reservation.reservation_date = DateTime.Today;
            DataProvider.Ins.db.RoomServices.Add(roomservice);
            DataProvider.Ins.db.Reservations.Add(reservation);
            DataProvider.Ins.db.Rooms.First(x => x.room_status_id == 2 && x.room_id == reservation.room_id).room_status_id = 1;
            DataProvider.Ins.db.SaveChanges();


            MessageBox.Show("Thêm Thành Công");
            FrameworkElement window = GetWindowParent(p);
            var w = window as Window;
            if (w != null)
            {
                w.Close();
            }
        }
        FrameworkElement GetWindowParent(Button p)
        {
            FrameworkElement parent = p;
            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }
            return parent;
        }
        void AddRoomListToComboBox()
        {
            RoomNameList.Clear();
            var roomList = DataProvider.Ins.db.Rooms.ToList();
            foreach (var item in roomList)
            {
                AddHireModel newRoom = new AddHireModel();
                if (item.room_status_id.ToString() == "2")
                {
                    newRoom.room = item;
                    RoomNameList.Add(newRoom);
                }
            }
        }
    }
}
