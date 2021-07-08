using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QuanLyKhachSan.Model;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using System.Globalization;

namespace QuanLyKhachSan.ViewModel
{
    public class CheckoutViewModel:BaseViewModel
    {

        public ICommand Window_IsLoaded { get; set; }
        public ICommand DetailCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        private string _SearchIP { get; set; }
        public string SearchIP
        {
            get => _SearchIP;
            set
            {
                _SearchIP = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PaymentHistoryModel> _ReservationList = new ObservableCollection<PaymentHistoryModel>();
        public ObservableCollection<PaymentHistoryModel> ReservationList
        {
            get => _ReservationList;
            set
            {
                _ReservationList = value;
                OnPropertyChanged();
            }
        }
        private PaymentHistoryModel _SelectedItemIP { get; set; }
        public PaymentHistoryModel SelectedItemIP
        {
            get => _SelectedItemIP;
            set
            {
                _SelectedItemIP = value;
                OnPropertyChanged();
            }
        }
        public CheckoutViewModel()
        {
            Window_IsLoaded = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                LoadData();
            });
            DetailCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                
            });

            SearchCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
            });
        }

        private static T FindParent<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);
            if (parent == null) return null;
            var parentT = parent as T;
            return parentT ?? FindParent<T>(parent);
        }
        void LoadData()
        {
            ReservationList = new ObservableCollection<PaymentHistoryModel>();
            try
            {
                var reservationList = DataProvider.Ins.db.Reservations.ToList();
                foreach(Reservation item in reservationList)
                {
                    PaymentHistoryModel paymenthistorymodel = new PaymentHistoryModel();
                    if (item.paid == 0) continue;
                    paymenthistorymodel.reservation = item;
                    var guestList = DataProvider.Ins.db.Guests;
                    Guest guest = guestList.FirstOrDefault(x => x.guest_id == paymenthistorymodel.reservation.guest_id);
                    Room room = DataProvider.Ins.db.Rooms.FirstOrDefault(x => x.room_id == paymenthistorymodel.reservation.room_id);
                    paymenthistorymodel.GuestName = guest.full_name;
                    paymenthistorymodel.RoomName = room.room_name;
                    CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
                    paymenthistorymodel.Amount = double.Parse(paymenthistorymodel.reservation.amount.Value.ToString()).ToString("#,###", cul.NumberFormat);
                    ReservationList.Add(paymenthistorymodel);
                }
            }
            catch
            {

            }
        }

    }
}
