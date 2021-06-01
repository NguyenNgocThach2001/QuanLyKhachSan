using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.Model
{
    public class DataProvider
    {
        public QuanLyKhachSanEntities1 db { get; set; }
        private static DataProvider _Ins;
        public static DataProvider Ins
        {
            get
            {
                if (_Ins == null)
                    _Ins = new DataProvider();
                return _Ins;
            }
            set
            {
                _Ins = value;
            }
        }
        public DataProvider()
        {
            db = new QuanLyKhachSanEntities1();
        }
    }
}
