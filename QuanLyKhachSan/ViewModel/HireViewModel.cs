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

namespace QuanLyKhachSan.ViewModel
{
    public class HireViewModel:BaseViewModel
    {
        public ICommand SelectedItemChangedCommand { get; set; }
        private List<HireModel> _HireList;
        public List<HireModel> HireList
        {
            get => _HireList;
            set
            {
                _HireList = value; OnPropertyChanged();
            }
        }

        private object _SelectedItemIP { get; set; }
        public object SelectedItemIP
        {
            get => _SelectedItemIP;
            set
            {
                _SelectedItemIP = value;
                OnPropertyChanged();
            }
        }
        #region commands
        public ICommand AddHireCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand DetailCommand { get; set; }
        public ICommand PaymentCommand { get; set; }
        public ICommand SearchTextBoxChangedCommand { get; set; }
        public ICommand Window_IsLoaded { get; set; }
        #endregion
        private string _SearchText { get; set; }
        private string SearchText {
            get => _SearchText;
            set
            {
                _SearchText = value;
                OnPropertyChanged();
            }
        }
        public HireViewModel()
        {
            Window_IsLoaded = new RelayCommand<Window>((p) => { return p != null; }, (p) =>
            {
                LoadHireList();
            });

            SelectedItemChangedCommand = new RelayCommand<Button>((p) => { return p != null; }, (p) =>
            {
            });

            AddHireCommand = new RelayCommand<Button>((p) => { return p != null; }, (p) => 
            {
                AddHireWindow addhireWindow = new AddHireWindow();
                addhireWindow.ShowDialog();
                addhireWindow.Close();
                LoadHireList();
            });

            DetailCommand = new RelayCommand<Button>((p) => { return p != null; }, (p) =>
            {
                
            }); 
            
            PaymentCommand = new RelayCommand<Button>((p) => { return p != null; }, (p) =>
            {
                ListBox lb = FindParent<ListBox>(p);
                int Reservation_id = new int();
                if ((lb.SelectedItem as HireModel)!=null)
                    if((lb.SelectedItem as HireModel).reservation != null)
                        Reservation_id = (lb.SelectedItem as HireModel).reservation.Reservation_id;
                if (Reservation_id != 0)
                {
                    PaymentWindow paymentWindow = new PaymentWindow();
                    ((PaymentWindowViewModelPopup)(paymentWindow.DataContext)).Reservation_id = Reservation_id;
                    paymentWindow.ShowDialog();
                    LoadHireList();
                }
            });

            SearchCommand = new RelayCommand<Button>((p) => { return p != null; }, (p) =>
            {
                LoadHireList();
            });
            SearchTextBoxChangedCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) => { SearchText = p.Text; });
        }

        private static T FindParent<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);
            if (parent == null) return null;
            var parentT = parent as T;
            return parentT ?? FindParent<T>(parent);
        }

        void LoadHireList()
        {
            HireList = new List<HireModel>();
            var hireList = DataProvider.Ins.db.Reservations.ToList();
            var guestList = DataProvider.Ins.db.Guests.ToList();
            var roomList = DataProvider.Ins.db.Rooms.ToList();
            foreach(var item in hireList)
            {
                try
                {
                    int tmpguest_id = guestList.FirstOrDefault(x => x.guest_id == item.guest_id).guest_id;
                    HireModel newHire = new HireModel();
                    newHire.guest_name = guestList.FirstOrDefault(x => x.guest_id == item.guest_id).full_name;
                    newHire.room_name = roomList.FirstOrDefault(x => x.room_id == item.room_id).room_name;
                    newHire.CheckinDate = (DateTime)item.check_in_date;
                    newHire.CheckoutDate = (DateTime)item.check_out_date;
                    newHire.CheckinDate = DateTime.Parse(((DateTime)item.check_in_date).ToString());
                    newHire.CheckoutDate = DateTime.Parse(((DateTime)item.check_out_date).ToString());
                    newHire.reservation = hireList.FirstOrDefault(x => x.guest_id == tmpguest_id);
                    //MessageBox.Show(newHire.CheckinDate.ToString());
                    HireList.Add(newHire);
                }
                catch { }
            }
        }
    }
}
