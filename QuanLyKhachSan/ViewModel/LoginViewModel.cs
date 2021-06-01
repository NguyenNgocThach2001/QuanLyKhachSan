using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.Security.Cryptography;
using QuanLyKhachSan.Model;

namespace QuanLyKhachSan.ViewModel
{
    public class LoginViewModel:BaseViewModel
    {
        #region commands
        public ICommand loginDone_btnClick { get; set; }
        public ICommand loginClose_btnClick { get; set; }
        #endregion
        public bool IsLogin { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangeCommand { get; set; }
        private string _UserName;
        public string UserName
        {
            get => _UserName;
            set { _UserName = value; OnPropertyChanged(); }
        }

        private string _Password;
        public string Password
        {
            get => _Password;
            set { _Password = value; OnPropertyChanged(); }
        }

        public LoginViewModel()
        {
            IsLogin = false;
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });
            PasswordChangeCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
        }
        void Login(Window p)
        {
            if (p == null) return;
            if (UserName == null) return;
            if (Password == null) return;
            string passEncode = MD5Hash(Base64Encode(Password));
            var init = DataProvider.Ins.db.Accounts.ToList().Count();
            if (init == 0)
            {
                Account ac = new Account();
                ac.UserName = UserName;
                ac.Password = MD5Hash(Base64Encode(Password));
                DataProvider.Ins.db.Accounts.Add(ac);
                DataProvider.Ins.db.SaveChanges();
                IsLogin = true;
                p.Close();
            }
            else
            {   
                var check = DataProvider.Ins.db.Accounts.Where(x => x.UserName == UserName && x.Password == passEncode).Count();
                if (check > 0)
                {
                    IsLogin = true;
                    p.Close();
                }
                else
                {
                    IsLogin = false;
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu");
                }
            }

        }
        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}
