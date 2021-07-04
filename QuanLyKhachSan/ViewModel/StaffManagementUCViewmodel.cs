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
    public class StaffManagementUCViewmodel : BaseViewModel
    {
        public ICommand window_IsLoaded { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateCommand { get; set; }

        public ICommand SexChangedCommand { get; set; }
        public ICommand DepartmentChangedCommand { get; set; }
        public ICommand PayrangeChangedCommand { get; set; }
        public ICommand PositionChangedCommand { get; set; }



        private ObservableCollection<SexModel> _SexCBList = new ObservableCollection<SexModel>();
        public ObservableCollection<SexModel> SexCBList
        {
            get => _SexCBList;
            set
            {
                _SexCBList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<StaffModel> _StaffList = new ObservableCollection<StaffModel>();
        public ObservableCollection<StaffModel> StaffList
        {
            get => _StaffList;
            set
            {
                _StaffList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DepartmentComboBoxModel> _DepartmentCBList = new ObservableCollection<DepartmentComboBoxModel>();
        public ObservableCollection<DepartmentComboBoxModel> DepartmentCBList
        {
            get => _DepartmentCBList;
            set
            {
                _DepartmentCBList = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<PositionModelComboBox> _PositionCBList = new ObservableCollection<PositionModelComboBox>();
        public ObservableCollection<PositionModelComboBox> PositionCBList
        {
            get => _PositionCBList;
            set
            {
                _PositionCBList = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<PayrangeComboBoxModel> _PayrangeCBList = new ObservableCollection<PayrangeComboBoxModel>();
        public ObservableCollection<PayrangeComboBoxModel> PayrangeCBList
        {
            get => _PayrangeCBList;
            set
            {
                _PayrangeCBList = value;
                OnPropertyChanged();
            }
        }

        private PayrangeComboBoxModel _PayrangeIP = new PayrangeComboBoxModel();
        public PayrangeComboBoxModel PayrangeIP
        {
            get => _PayrangeIP;
            set
            {
                _PayrangeIP = value;
                OnPropertyChanged();
            }
        }
        private DepartmentComboBoxModel _DepartmentIP = new DepartmentComboBoxModel();
        public DepartmentComboBoxModel DepartmentIP
        {
            get => _DepartmentIP;
            set
            {
                _DepartmentIP = value;
                OnPropertyChanged();
            }
        }
        private PositionModelComboBox _PositionIP = new PositionModelComboBox();
        public PositionModelComboBox PositionIP
        {
            get => _PositionIP;
            set
            {
                _PositionIP = value;
                OnPropertyChanged();
            }
        }

        private SexModel _SSex = new SexModel();
        public SexModel SSex
        {
            get => _SSex;
            set
            {
                _SSex = value;
                OnPropertyChanged();
            }
        }


        private DateTime _BirthdayIP = new DateTime();
        public DateTime BirthdayIP
        {
            get => _BirthdayIP;
            set
            {
                _BirthdayIP = value;
                OnPropertyChanged();
            }
        }


        private string _StaffNameIP { get; set; }
        public string StaffNameIP
        {
            get => _StaffNameIP;
            set
            {
                _StaffNameIP = value;
                OnPropertyChanged();
            }
        }



        private string _PhoneIP { get; set; }
        public string PhoneIP
        {
            get => _PhoneIP;
            set
            {
                _PhoneIP = value;
                OnPropertyChanged();
            }
        }


        private DateTime _ContractDateIP = new DateTime();
        public DateTime ContractDateIP
        {
            get => _ContractDateIP;
            set
            {
                _ContractDateIP = value;
                OnPropertyChanged();
            }
        }

        private string _NoteIP { get; set; }
        public string NoteIP
        {
            get => _NoteIP;
            set
            {
                _NoteIP = value;
                OnPropertyChanged();
            }
        }

        public StaffManagementUCViewmodel()
        {
            window_IsLoaded = new RelayCommand<UserControl>((p) => { return true; }, (p) => {
                LoadData();
                init();
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
            SexChangedCommand = new RelayCommand<ComboBox>((p) => { return p != null; },
                                     (p) => {
                                         try
                                         {
                                             SSex = (SexModel)p.SelectedItem;
                                         }
                                         catch { };
                                     });
            DepartmentChangedCommand = new RelayCommand<ComboBox>((p) => { return p != null; },
                                     (p) => {
                                         try
                                         {
                                             DepartmentIP = ((DepartmentComboBoxModel)p.SelectedItem);
                                         }
                                         catch { };
                                     });
            PositionChangedCommand = new RelayCommand<ComboBox>((p) => { return p != null; },
                                     (p) => {
                                         try
                                         {
                                             PositionIP = ((PositionModelComboBox)p.SelectedItem);
                                         }
                                         catch { };
                                     });
            PayrangeChangedCommand = new RelayCommand<ComboBox>((p) => { return p != null; },
                                     (p) => {
                                         try
                                         {
                                             PayrangeIP = ((PayrangeComboBoxModel)p.SelectedItem);
                                         }
                                         catch { };
                                     });
        }

        void init()
        {
            BirthdayIP = DateTime.Now;
            ContractDateIP = DateTime.Now;
        }

        void LoadData()
        {
            int count = 0;
            StaffList = new ObservableCollection<StaffModel>();
            var stafflist = DataProvider.Ins.db.Staffs.Where(x => x.Staff_Name != "").ToList();
            foreach (var staff in stafflist)
            {
                try
                {
                    StaffModel tmp = new StaffModel();
                    tmp.STT = ++count;
                    tmp.ID = staff.Staff_ID;
                    tmp.Name = staff.Staff_Name;
                    tmp.Sex = staff.Staff_Sex;
                    tmp.Phone = staff.Staff_Phone;
                    tmp.Position_ID = staff.Staff_Position_ID;
                    tmp.Position_Name = staff.Position.Position_Name;
                    tmp.Payrange_ID = staff.Staff_Payrange_ID;
                    tmp.Payrange = staff.Payrange.Payrange_Num.Value;
                    tmp.Coefficents_ID = staff.Staff_Coefficients_ID.ToString();
                    tmp.Department_ID = staff.Department.Department_ID;
                    tmp.Department_Name = staff.Department.Department_Name;
                    tmp.Contract_Date = staff.Staff_Contract_Date.Value;
                    if (staff.Expiry_Date != null)
                        tmp.Expiry_Date = staff.Expiry_Date.Value;
                    tmp.Birthday = staff.Birthday.Value;
                    tmp.Note = NoteIP;
                    StaffList.Add(tmp);
                }
                catch {
                    count--;
                }
            }
            AddDepartmentToComboBox();
            AddPayrangeToComboBox();
            AddPositionToComboBox();
            AddSexToComboBox();
        }

        void AddSexToComboBox()
        {
            SexCBList.Clear();
            List<string> p = new List<string>{ "Nam", "Nữ", "Khác" };
            foreach (string item in p)
            {
                SexModel sex = new SexModel();
                sex.Sex = item;
                SexCBList.Add(sex);
            }
        }
        void AddDepartmentToComboBox()
        {
            DepartmentCBList.Clear();
            var departmentList = DataProvider.Ins.db.Departments.ToList();
            foreach (var item in departmentList)
            {
                DepartmentComboBoxModel department = new DepartmentComboBoxModel();
                department.ID = item.Department_ID;
                department.Name = item.Department_Name;
                DepartmentCBList.Add(department);
            }
        }
        void AddPayrangeToComboBox()
        {
            PayrangeCBList.Clear();
            var payrangeList = DataProvider.Ins.db.Payranges.ToList();
            foreach (var item in payrangeList)
            {
                PayrangeComboBoxModel payrange = new PayrangeComboBoxModel();
                payrange.ID = item.Payrange_ID;
                payrange.Num = item.Payrange_Num.Value;
                PayrangeCBList.Add(payrange);
            }
        }

        void AddPositionToComboBox()
        {
            PositionCBList.Clear();
            var positionList = DataProvider.Ins.db.Positions.ToList();
            foreach (var item in positionList)
            {
                PositionModelComboBox position = new PositionModelComboBox();
                position.ID = item.Position_ID;
                position.Name = item.Position_Name;
                PositionCBList.Add(position);
            }
        }
        void DeleteData(Button p)
        {
            Lib trash = new Lib();
            FrameworkElement window = trash.GetWindowParent(p);
            var w = trash.FindVisualChildByName<DataGrid>(window, "dataGrid");
            if (w != null && (StaffModel)w.SelectedItem != null)
            {
                var db = DataProvider.Ins.db;
                Staff del = new Staff();
                del = db.Staffs.Where
                            (x => x.Staff_ID ==
                            ((StaffModel)w.SelectedItem).ID).First();
                db.Staffs.Remove(del);
                db.SaveChanges();
                LoadData();
            }
        }
        void UpdateData(Button p)
        {

        }
        void AddData(UserControl p)
        {
            try
            {
                if (StaffNameIP == "") return;
                var db = DataProvider.Ins.db;
                Staff add = new Staff();
                add.Staff_Name = StaffNameIP;
                add.Staff_Sex = SSex.Sex;
                add.Staff_Phone = PhoneIP;
                add.Staff_Position_ID = PositionIP.ID;
                add.Staff_Payrange_ID = PayrangeIP.ID;
                add.Staff_Department_ID = DepartmentIP.ID;
                add.Staff_Contract_Date = ContractDateIP;
                add.Birthday = BirthdayIP;
                add.Note = NoteIP;
                db.Staffs.Add(add);
                db.SaveChanges();
                LoadData();
            }
            catch
            {
                MessageBox.Show("Lỗi! Hãy kiểm tra lại thông tin!");
            }
        }
    }
}
