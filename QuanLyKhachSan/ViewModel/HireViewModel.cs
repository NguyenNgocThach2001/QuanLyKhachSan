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

        private ObservableCollection<HireModel> _HireList;
        public ObservableCollection<HireModel> HireList
        {
            get => _HireList;
            set
            {
                _HireList = value; OnPropertyChanged();
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
            Window_IsLoaded = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                LoadHireList();
            });

            AddHireCommand = new RelayCommand<Button>((p) => { return p == null ? false : true; }, (p) => 
            {
                AddHireWindow addhireWindow = new AddHireWindow();
                addhireWindow.ShowDialog();
                addhireWindow.Close();
                LoadHireList();
            });

            SearchCommand = new RelayCommand<Button>((p) => { return p == null ? false : true; }, (p) =>
            {
                LoadHireList();
            });
            SearchTextBoxChangedCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) => { SearchText = p.Text; });
        }

        void LoadHireList()
        {
            HireList = new ObservableCollection<HireModel>();
            var hireList = DataProvider.Ins.db.Reservations.ToList();
            var guestList = DataProvider.Ins.db.Guests.ToList();
            var roomList = DataProvider.Ins.db.Rooms.ToList();
            foreach(var item in hireList)
            {
                HireModel newHire = new HireModel();
                newHire.guest_name = guestList.FirstOrDefault(x => x.guest_id == item.guest_id).full_name;
                newHire.room_name = roomList.FirstOrDefault(x => x.room_id == item.room_id).room_name;
                newHire.CheckinDate = (DateTime)item.check_in_date;
                newHire.CheckoutDate = (DateTime)item.check_out_date;
                newHire.CheckinDate = DateTime.Parse(((DateTime)item.check_in_date).ToString());
                newHire.CheckoutDate = DateTime.Parse(((DateTime)item.check_out_date).ToString());
                //MessageBox.Show(newHire.CheckinDate.ToString());
                HireList.Add(newHire);
            }
        }
        
        
    }
}
