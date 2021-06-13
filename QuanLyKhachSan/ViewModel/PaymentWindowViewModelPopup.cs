﻿using System;
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
        #endregion
        public int SelectedItem { get; set; }
        public PaymentWindowViewModelPopup()
        {
            Window_IsLoaded = new RelayCommand<Window>((p) => { return p != null; }, (p) => {
                LoadData();
            });
        }

        public void LoadData()
        {
            MessageBox.Show(SelectedItem.ToString());
        }
    }
}