using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKhachSan.ViewModel
{
    public class QuanLyDuLieuViewModel:BaseViewModel
    {
        public ICommand RoomStatusCommand { get; set; }
        public ICommand RoomTypeCommand { get; set; }
        public ICommand RoomCommand { get; set; }
        public ICommand ServiceCommand { get; set; }
        public ICommand StaffCommand { get; set; }
        public ICommand DepartmentCommand { get; set; }
        public ICommand window_IsLoaded { get; set; }
        private int _RoomIndex { get; set; }
        public int RoomIndex { 
            get => _RoomIndex; 
            set {
                _RoomIndex = value;
                OnPropertyChanged();
            }
        }

        private int _RoomTypeIndex { get; set; }
        public int RoomTypeIndex
        {
            get => _RoomTypeIndex;
            set
            {
                _RoomTypeIndex = value;
                OnPropertyChanged();
            }
        }

        private int _RoomStatusIndex { get; set; }
        public int RoomStatusIndex
        {
            get => _RoomStatusIndex;
            set
            {
                _RoomStatusIndex = value;
                OnPropertyChanged();
            }
        }
        private int _ServiceIndex { get; set; }
        public int ServiceIndex
        {
            get => _ServiceIndex;
            set
            {
                _ServiceIndex = value;
                OnPropertyChanged();
            }
        }

        private int _StaffIndex { get; set; }
        public int StaffIndex
        {
            get => _StaffIndex;
            set
            {
                _StaffIndex = value;
                OnPropertyChanged();
            }
        }

        private int _DepartmentIndex { get; set; }
        public int DepartmentIndex
        {
            get => _DepartmentIndex;
            set
            {
                _DepartmentIndex = value;
                OnPropertyChanged();
            }
        }
        public QuanLyDuLieuViewModel()
        {
            window_IsLoaded = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                LoadRoomManagementUC();
            });

            RoomCommand = new RelayCommand<Button>((p) => { return p == null ? false : true; }, (p) =>
            {
                LoadRoomManagementUC();
            });


            RoomTypeCommand = new RelayCommand<Button>((p) => { return p == null ? false : true; }, (p) =>
            {
                LoadRoomTypeManagementUC();
            });

            RoomStatusCommand = new RelayCommand<Button>((p) => { return p == null ? false : true; }, (p) =>
            {
                LoadRoomStatusManagementUC();
            });

            StaffCommand = new RelayCommand<Button>((p) => { return p == null ? false : true; }, (p) =>
            {
                LoadStaffManagementUC();
            });

            ServiceCommand = new RelayCommand<Button>((p) => { return p == null ? false : true; }, (p) =>
            {
                LoadServiceManagementUC();
            });

            DepartmentCommand = new RelayCommand<Button>((p) => { return p == null ? false : true; }, (p) =>
            {
                LoadDepartmentManagementUC();
            });
        }

        void init()
        {
            RoomIndex = 0;
            DepartmentIndex = 0;
            StaffIndex = 0;
            ServiceIndex = 0;
            RoomStatusIndex = 0;
            RoomTypeIndex = 0;
        }

        void LoadRoomManagementUC()
        {
            init();
            RoomIndex = 3;
        }

        void LoadRoomTypeManagementUC()
        {
            init();
            RoomTypeIndex = 3;
        }

        void LoadRoomStatusManagementUC()
        {
            init();
            RoomStatusIndex = 3;
        }
        void LoadStaffManagementUC()
        {
            init();
            StaffIndex = 3;
        }
        void LoadServiceManagementUC()
        {
            init();
            ServiceIndex = 3;
        }
        void LoadDepartmentManagementUC()
        {
            init();
            DepartmentIndex = 3;
        }
    }
}
