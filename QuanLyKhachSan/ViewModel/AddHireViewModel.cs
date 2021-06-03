using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLyKhachSan.Model;

namespace QuanLyKhachSan.ViewModel
{
    public class AddHireViewModel:BaseViewModel
    {
        #region commands
        public ICommand RoomComboBoxCommand;
        public ICommand GuestNameChangedCommand;
        public ICommand CMNDChangedCommand;
        public ICommand AddHireCommand;
        #endregion
        private Reservation _reservation { get; set; }
        public Reservation reservation
        {
            get => _reservation;
            set { 
                _reservation = value;
            }
        }
        public ObservableCollection<AddHireModel> AddHireList;

        private string _Selected { get; set; }
        public string Selected { 
            get => _Selected;
            set
            {
                _Selected = value;
                OnPropertyChanged();
            }
        }
        private string _GuestName { get; set; }
        public string GuestName { 
            get => _GuestName;
            set
            {
                _GuestName = value;
                OnPropertyChanged();
            }
        }
        private string _RoomName { get; set; }
        public string RoomName
        {
            get => _RoomName;
            set
            {
                _RoomName = value;
                OnPropertyChanged();
            }
        }

        private string _CMND { get; set; }
        public string CMND
        {
            get => _CMND;
            set
            {
                _CMND = value;
                OnPropertyChanged();
            }
        }

        private string _CheckinDate { get; set; }
        public string CheckinDate
        {
            get => _CheckinDate;
            set
            {
                _CheckinDate = value;
                OnPropertyChanged();
            }
        }


        private string _CheckoutDate { get; set; }
        public string CheckoutDate
        {
            get => _CheckoutDate;
            set
            {
                _CheckoutDate = value;
                OnPropertyChanged();
            }
        }

        public AddHireViewModel()
        {

            AddHireCommand = new RelayCommand<Button>((p) => { return p != null ? true : false; }, 
                                                      (p) => AddHireToDatabase());
            GuestNameChangedCommand = new RelayCommand<ComboBox>((p) => { return p != null ? true : false; },
                                                                 (p) => { });
        }

        void AddHireToDatabase()
        {
            RoomNameList = new ObservableCollection<AddHireModel>();
            var roomList = DataProvider.Ins.db.Rooms.ToList();
            foreach(var item in roomList)
            {
                Room newRoom = new Room();
                newRoom.room_name
            }
        }

    }
}
