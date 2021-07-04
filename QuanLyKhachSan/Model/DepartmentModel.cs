using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.Model
{
    public class DepartmentModel
    {
        public int STT { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public int Head_ID { get; set; }
        public string Head_Name { get; set; }
        public string Deputy_ID { get; set; }
        public string Deputy_Name { get; set; }
    }
}
