using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.Security.Cryptography;

namespace QuanLyKhachSan.ViewModel
{
    public class LoginViewModel:BaseViewModel
    {
        #region commands
        public ICommand loginDone_btnClick { get; set; }
        public ICommand loginClose_btnClick { get; set; }
        #endregion
        public bool IsLogin { get; set; }
        public LoginViewModel()
        {
            loginDone_btnClick = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                var HashCodePassword = MD5Hash("a");
                IsLogin = true;
                p.Close();
            }
            );
        }
        public static string MD5Hash(string text)
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
