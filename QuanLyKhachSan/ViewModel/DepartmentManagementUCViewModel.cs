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
    public class DepartmentManagementUCViewModel:BaseViewModel
    {

        public ICommand window_IsLoaded { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand HeadChangedCommand { get; set; }
        public ICommand DeputyChangedCommand { get; set; }
        private ObservableCollection<DepartmentModel> _DepartmentList { get; set; }
        public ObservableCollection<DepartmentModel> DepartmentList
        {
            get => _DepartmentList;
            set
            {
                _DepartmentList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<StaffModel> _HeadCBList = new ObservableCollection<StaffModel>();
        public ObservableCollection<StaffModel> HeadCBList
        {
            get => _HeadCBList;
            set
            {
                _HeadCBList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<StaffModel> _DeputyCBList = new ObservableCollection<StaffModel>();
        public ObservableCollection<StaffModel> DeputyCBList
        {
            get => _DeputyCBList;
            set
            {
                _DeputyCBList = value;
                OnPropertyChanged();
            }
        }


        private string _DepartmentNameIP { get; set; }
        public string DepartmentNameIP
        {
            get => _DepartmentNameIP;
            set
            {
                _DepartmentNameIP = value;
                OnPropertyChanged();
            }
        }

        private StaffModel _HeadIP { get; set; }
        public StaffModel HeadIP
        {
            get => _HeadIP;
            set
            {
                _HeadIP = value;
                OnPropertyChanged();
            }
        }

        private StaffModel _DeputyIP { get; set; }
        public StaffModel DeputyIP
        {
            get => _DeputyIP;
            set
            {
                _DeputyIP = value;
                OnPropertyChanged();
            }
        }
        private string _DepartmentIDIP { get; set; }
        public string DepartmentIDIP
        {
            get => _DepartmentIDIP;
            set
            {
                _DepartmentIDIP = value;
                OnPropertyChanged();
            }
        }

        public DepartmentManagementUCViewModel()
        {
            window_IsLoaded = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {
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
            HeadChangedCommand = new RelayCommand<ComboBox>((p) => { return p != null; },
                                     (p) => {
                                         try
                                         {
                                             HeadIP = (StaffModel)p.SelectedItem;
                                         }
                                         catch { };
                                     });
            DeputyChangedCommand = new RelayCommand<ComboBox>((p) => { return p != null; },
                                     (p) => {
                                         try
                                         {
                                             DeputyIP = ((StaffModel)p.SelectedItem);
                                         }
                                         catch { };
                                     });
        }

        void AddData(UserControl p)
        {
            try
            {
                if (DepartmentNameIP == "" || DepartmentIDIP == "") return;
                var db = DataProvider.Ins.db;
                Department add = new Department();
                add.Department_Name = DepartmentNameIP;
                add.Department_ID = DepartmentIDIP;
                add.Department_Head_id = HeadIP.ID;
                add.Deputy = DeputyIP.ID.ToString();
                db.Departments.Add(add);
                db.SaveChanges();
                LoadData();
            }
            catch
            {
                MessageBox.Show("Lỗi! Hãy kiểm tra lại thông tin nhập!");
            }
        }

        void DeleteData(Button p)
        {
            Lib trash = new Lib();
            FrameworkElement window = trash.GetWindowParent(p);
            var w = trash.FindVisualChildByName<DataGrid>(window, "dataGrid");
            if (w != null && (DepartmentModel)w.SelectedItem != null)
            {
                var db = DataProvider.Ins.db;
                Department del = new Department();
                del = db.Departments.Where
                            (x => x.Department_ID ==
                            ((DepartmentModel)w.SelectedItem).ID).First();
                db.Departments.Remove(del);
                db.SaveChanges();
                LoadData();
            }
        }

        void UpdateData(Button p)
        {

        }

        void LoadData()
        {
            DepartmentList = new ObservableCollection<DepartmentModel>();
            int count = 0;
            var departmentlist = DataProvider.Ins.db.Departments.ToList();
            var stafflist = DataProvider.Ins.db.Staffs.ToList();
            foreach (var department in departmentlist)
            {
                try
                {
                    DepartmentModel tmp = new DepartmentModel();
                    tmp.STT = ++count;
                    tmp.ID = department.Department_ID;
                    tmp.Name = department.Department_Name;
                    if (department.Department_Head_id != null)
                    {
                        tmp.Head_ID = department.Department_Head_id.Value;
                        tmp.Head_Name = stafflist.First(x => x.Staff_ID == tmp.Head_ID).Staff_Name;
                    }
                    if (department.Deputy != null)
                    {
                        tmp.Deputy_ID = department.Deputy;
                        tmp.Deputy_Name = stafflist.First(x => x.Staff_ID.ToString() == tmp.Deputy_ID).Staff_Name;
                    }
                    DepartmentList.Add(tmp);
                }
                catch
                {
                    count--;
                }
            }
            AddDataToComboBox();
        }

        void AddDataToComboBox()
        {
            HeadCBList.Clear();
            DeputyCBList.Clear();
            var List = DataProvider.Ins.db.Staffs.ToList();
            foreach (var item in List)
            {
                StaffModel staff = new StaffModel();
                staff.ID = item.Staff_ID;
                staff.Name = item.Staff_Name;
                HeadCBList.Add(staff);
                DeputyCBList.Add(staff);
            }
        }
    }
}
