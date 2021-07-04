using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.Model
{
    public class DepartmentModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Head_ID { get; set; }
        public int Deputy_ID { get; set; }
    }
}
