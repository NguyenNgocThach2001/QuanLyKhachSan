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
    public class PaymentWindowViewModelPopup:BaseViewModel
    {
        #region commands
        public ICommand Window_IsLoaded { get; set; }
        public ICommand AddServiceCommand { get; set; }
        public ICommand DeleteServiceCommand { get; set; }
        public ICommand ServiceNameChangedCommand { get; set; }
        public ICommand PayCommand { get; set; }
        #endregion
        private string _GuestName { get; set; }
        public string GuestName
        {
            get => _GuestName;
            set
            {
                _GuestName = value;
                OnPropertyChanged();
            }
        }
        private double _Sum { get; set; }
        public double Sum
        {
            get => _Sum;
            set
            {
                _Sum = value;
                OnPropertyChanged();
            }
        }
        private int _UseageTxtBox { get; set; }
        public int UseageTxtBox
        {
            get => _UseageTxtBox;
            set
            {
                _UseageTxtBox = value;
                OnPropertyChanged();
                AmountUpdate();
            }
        }
        

        private string _AmountTxtBlock { get; set; }
        public string AmountTxtBlock
        {
            get => _AmountTxtBlock;
            set
            {
                _AmountTxtBlock = value;
                OnPropertyChanged();
            }
        }

        private string _UnitTxtBlock { get; set; }
        public string UnitTxtBlock
        {
            get => _UnitTxtBlock;
            set
            {
                _UnitTxtBlock = value;
                OnPropertyChanged();
            }
        }

        private string _UnitPriceTxtBlock { get; set; }
        public string UnitPriceTxtBlock
        {
            get => _UnitPriceTxtBlock;
            set
            {
                _UnitPriceTxtBlock = value;
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
        private DateTime _CheckinDate = new DateTime();
        public DateTime CheckinDate
        {
            get => _CheckinDate;
            set
            {
                _CheckinDate = value;
                OnPropertyChanged();
            }
        }
        private DateTime _CheckoutDate = new DateTime();
        public DateTime CheckoutDate
        {
            get => _CheckoutDate;
            set
            {
                _CheckoutDate = value;
                OnPropertyChanged();
            }
        }

        private ServiceModel _SService = new ServiceModel();
        public ServiceModel SService
        {
            get => _SService;
            set
            {
                _SService = value;
                OnPropertyChanged();
            }
        }

        private DataGridRowDetailsVisibilityMode _rowDetailsVisible;
        public DataGridRowDetailsVisibilityMode RowDetailsVisible
        {
            get { return _rowDetailsVisible; }
            set
            {
                _rowDetailsVisible = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ServicePaymentModel> _ServicePaymentList = new ObservableCollection<ServicePaymentModel>();
        public ObservableCollection<ServicePaymentModel> ServicePaymentList
        {
            get => _ServicePaymentList;
            set
            {
                _ServicePaymentList = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<ServiceModel> _ServiceCBList = new ObservableCollection<ServiceModel>();
        public ObservableCollection<ServiceModel> ServiceCBList
        {
            get => _ServiceCBList;
            set
            {
                _ServiceCBList = value;
                OnPropertyChanged();
            }
        }
        private int _Reservation_id { get; set; }
        public int Reservation_id
        {
            get => _Reservation_id;
            set
            {
                _Reservation_id = value;
                LoadData();
            }
        }
        public PaymentWindowViewModelPopup()
        {
            Window_IsLoaded = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                LoadData();
            });
            AddServiceCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                AddData();
            });
            DeleteServiceCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                DeleteData(p);
            });

            ServiceNameChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                try
                {
                    if (SService == null) return;
                    UnitPriceTxtBlock = SService.unitPrice.ToString();
                    UnitTxtBlock = SService.unit;
                    Lib trash = new Lib();
                    AmountTxtBlock = trash.VN((UseageTxtBox * SService.unitPrice).ToString());
                }
                catch
                {
                    UnitPriceTxtBlock = "";
                    UnitTxtBlock = "";
                    AmountTxtBlock = "";
                }
            });

            PayCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                Pay(p);
            });
        }

        void Pay(Button p)
        {
            var db = DataProvider.Ins.db;
            Reservation reservation = db.Reservations.FirstOrDefault(x => x.Reservation_id == Reservation_id);
            Room room = db.Rooms.FirstOrDefault(x => x.room_id == reservation.room_id);
            room.room_status_id = 2;
            reservation.paid = 1;
            db.SaveChanges();
            MessageBox.Show("Thanh toán thành công");
            Lib trash = new Lib();
            FrameworkElement window = trash.GetWindowParent(p);
            var w = window as Window;
            if (w != null)
            {
                w.Close();
            }
        }

        void AddData() 
        {
            if (SService == null) return;
            if (SService.serviceName == null) return;
            if (SService.serviceName == "") return;
            try
            {
                PaymentService paymentservice = new PaymentService();
                paymentservice.Service_ID = SService.serviceID;
                paymentservice.Reservation_id = Reservation_id;
                paymentservice.Useage = UseageTxtBox;
                var db = DataProvider.Ins.db; 
                db.PaymentServices.Add(paymentservice);
                db.SaveChanges();
                LoadData();
            }
            catch
            {
                MessageBox.Show("Lỗi!Hãy kiểm tra lại thông tin!");
            }
        }
        void DeleteData(Button p)
        {
            if (p == null)
                return;
            DataGrid w = FindParent<DataGrid>(p);
            if (w != null && (ServicePaymentModel)w.SelectedItem != null)
            {
                if (((ServicePaymentModel)w.SelectedItem).ID == 0) return;
                var db = DataProvider.Ins.db;
                PaymentService del = new PaymentService();
                del = db.PaymentServices.Where
                            (x => x.PaymentService_ID ==
                            ((ServicePaymentModel)w.SelectedItem).ID).First();
                db.PaymentServices.Remove(del);
                db.SaveChanges();
                LoadData();
            }
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
            int cnt = 0;
            Sum = 0;
            UseageTxtBox = 1;
            try
            {
                Reservation reservation = DataProvider.Ins.db.Reservations.FirstOrDefault(x => x.Reservation_id == Reservation_id);
                var guestList = DataProvider.Ins.db.Guests;
                Guest guest = guestList.FirstOrDefault(x => x.guest_id == reservation.guest_id);
                Room room = DataProvider.Ins.db.Rooms.FirstOrDefault(x => x.room_id == reservation.room_id);
                GuestName = guest.full_name;
                CMND = guest.CMND;
                RoomName = room.room_name;
                if (reservation.check_in_date != null)
                    CheckinDate = reservation.check_in_date.Value;
                if (reservation.check_out_date != null)
                    CheckoutDate = reservation.check_out_date.Value;

                ServicePaymentList = new ObservableCollection<ServicePaymentModel>();
                var servicepaymentList = DataProvider.Ins.db.PaymentServices.Where
                                        (x => x.Reservation_id == reservation.Reservation_id).ToList();
                foreach(PaymentService item in servicepaymentList)
                {
                    try
                    {
                        Service service = DataProvider.Ins.db.Services.FirstOrDefault(x => x.Service_ID == item.Service_ID);
                        ServicePaymentModel tmp = new ServicePaymentModel();
                        tmp.ID = item.PaymentService_ID;
                        tmp.STT = ++cnt;
                        tmp.Unit = service.unit;
                        tmp.UnitPrice = service.unitPrice.Value;
                        tmp.ServiceName = service.Service_Name;
                        tmp.Useage = item.Useage.Value;
                        tmp.Sum = tmp.Useage * tmp.UnitPrice;
                        ServicePaymentList.Add(tmp);
                        Sum += tmp.Sum;
                    }
                    catch
                    {
                        --cnt;
                    }
                }
                ServicePaymentModel tmp1 = new ServicePaymentModel();
                RoomService roomService = DataProvider.Ins.db.RoomServices.FirstOrDefault(x => x.Room_Service == reservation.Room_Service);
                tmp1.ID = 0;
                tmp1.STT = ++cnt;
                tmp1.Unit = roomService.Unit;
                tmp1.UnitPrice = roomService.Unit_Price.Value;
                tmp1.ServiceName = roomService.Room_Name;
                tmp1.Useage = roomService.Useage.Value;
                tmp1.Sum = tmp1.Useage * tmp1.UnitPrice;
                ServicePaymentList.Add(tmp1);
                Sum += tmp1.Sum;
            }
            catch
            {
                
            }
            AddDataToCBBox();
        }

        void AddDataToCBBox()
        {
            ServiceCBList = new ObservableCollection<ServiceModel>();
            var serviceCBList = DataProvider.Ins.db.Services.Where
                                            (x => x.Service_Name != "").ToList();
            foreach (Service item in serviceCBList)
            {
                try
                {
                    ServiceModel tmp = new ServiceModel();
                    tmp.serviceID = item.Service_ID;
                    tmp.serviceName = item.Service_Name;
                    tmp.unit = item.unit;
                    tmp.unitPrice = item.unitPrice.Value;
                    ServiceCBList.Add(tmp);
                }
                catch { }
            }
        }
        void AmountUpdate()
        {
            try
            {
                Lib trash = new Lib();
                if (SService == null) return;
                AmountTxtBlock = trash.VN((UseageTxtBox * SService.unitPrice).ToString());
            }
            catch { }
        }
    }
}
