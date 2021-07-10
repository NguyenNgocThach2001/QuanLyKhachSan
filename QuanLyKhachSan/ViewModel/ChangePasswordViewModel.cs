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

    public class ChangePasswordViewModel:BaseViewModel
    {
        public ICommand VerifyCommand { get; set; }
        public ICommand CancelCommand { get; set; }
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

        private string _Password { get; set; }
        public string Password
        {
            get => _Password;
            set
            {
                _Password = value;
                OnPropertyChanged();
            }
        }

        private string _VerifyPassword { get; set; }
        public string VerifyPassword
        {
            get => _VerifyPassword;
            set
            {
                _VerifyPassword = value;
                OnPropertyChanged();
            }
        }

        public ChangePasswordViewModel()
        {
            VerifyCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Verify(p);
            });
            CancelCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Cancel(p);
            });
        }
        void Verify(Window p) 
        { 

        }
        void Cancel(Window p)
        {

        }
    }
}
