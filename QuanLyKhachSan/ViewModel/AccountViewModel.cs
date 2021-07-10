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
    public class AccountViewModel:BaseViewModel
    {

        public ICommand Window_IsLoaded { get; set; }
        public ICommand SelectAccountCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }
        public ICommand DeleteAccountCommand { get; set; }

        private ObservableCollection<AccountModel> _AccountList = new ObservableCollection<AccountModel>();
        public ObservableCollection<AccountModel> AccountList
        {
            get => _AccountList;
            set
            {
                _AccountList = value; 
                OnPropertyChanged();
            }
        }

        private AccountModel _SelectedItem = new AccountModel();
        public AccountModel SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
            }
        }


        private bool _IsChecked { get; set; }
        public bool IsChecked
        {
            get => _IsChecked;
            set
            {
                if (SelectedItem == null) ClearDisplayData();
                _IsChecked = value;
                if (_IsChecked)
                {
                    Check();
                }
                else
                {
                    UnCheck();
                }
                OnPropertyChanged();
            }
        }

        private string _UserName { get; set; }
        public string UserName
        {
            get => _UserName;
            set
            {
                _UserName = value;
                OnPropertyChanged();
            }
        }
        private string _StaffName { get; set; }
        public string StaffName
        {
            get => _StaffName;
            set
            {
                if (SelectedItem == null) ClearDisplayData();
                _StaffName = value;
                OnPropertyChanged();
            }
        }


        public AccountViewModel()
        {
            Window_IsLoaded = new RelayCommand<Window>((p) => { return true; }, (p) => 
            {
                LoadAccountList();
            });
            SelectAccountCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                LoadSelectedAccount(p);
            });
            ChangePasswordCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                ChangePassword(p);
            });
            DeleteAccountCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                DeleteAccount(p);
            });
        }

        void DeleteAccount(Window p)
        {

        }

        void ChangePassword(Window p)
        {
            if (SelectedItem == null) return;
            ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow();
            ((ChangePasswordViewModel)changePasswordWindow.DataContext).UserName = SelectedItem.account.UserName;
            changePasswordWindow.ShowDialog();
        }
        void LoadAccountList()
        {
            AccountList = new ObservableCollection<AccountModel>();
            int cnt = 0;
            var account = DataProvider.Ins.db.Accounts.ToList();
            foreach(Account item in account)
            {
                try
                {
                    AccountModel tmp = new AccountModel();
                    tmp.STT = ++cnt;
                    tmp.account = item;
                    AccountList.Add(tmp);
                }
                catch
                {
                    cnt--;
                }
            }
        }

        void Check()
        {
            var db = DataProvider.Ins.db;
            Account account = db.Accounts.FirstOrDefault(x => x.UserName == SelectedItem.account.UserName);
            account.Role = "Admin";
            db.Accounts.Remove(account);
            db.SaveChanges();
            db.Accounts.Add(account);
            db.SaveChanges();
        }

        void ClearDisplayData()
        {
            StaffName = "";
            UserName = "";
        }
        void UnCheck()
        {
            var db = DataProvider.Ins.db;
            Account account = db.Accounts.FirstOrDefault(x => x.UserName == SelectedItem.account.UserName);
            account.Role = null;
            db.Accounts.Remove(account);
            db.SaveChanges();
            db.Accounts.Add(account);
            db.SaveChanges();
        }
        private static T FindParent<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);
            if (parent == null) return null;
            var parentT = parent as T;
            return parentT ?? FindParent<T>(parent);
        }

        void LoadSelectedAccount(Window p)
        {
            ListBox lb = FindParent<ListBox>(p);
            if ((SelectedItem as AccountModel) != null)
                if ((SelectedItem as AccountModel).account != null)
                    if ((SelectedItem as AccountModel).account.UserName != "")
                    {
                        UserName = SelectedItem.account.UserName;
                        StaffName = SelectedItem.account.StaffName;
                        if (SelectedItem.account.Role == "Admin")
                        {
                            IsChecked = true;
                        }
                        else
                        {
                            IsChecked = false;
                        }
                    }
        }
    }
}
