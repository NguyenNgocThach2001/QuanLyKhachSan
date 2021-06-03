using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QuanLyKhachSan.Model;

namespace QuanLyKhachSan.ViewModel
{
    public class AddHireViewModel:BaseViewModel
    {
        #region commands
        public ICommand RoomComboBoxCommand;
        public ICommand GuestNameChangedCommand;
        public ICommand CMNDChangedCommand;
        #endregion

        public ObservableCollection<AddHireModel> AddHireList;
        public AddHireViewModel()
        {

        }

    }
}
