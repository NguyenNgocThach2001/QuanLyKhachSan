using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.Model
{
    public class StaffModel
    {
        public int STT { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
        public string Position_ID { get; set; }
        public string Position_Name { get; set; }
        public string Payrange_ID { get; set; }
        public double Payrange { get; set; }
        public string Coefficents_ID { get; set; }
        public string Department_ID { get; set; }
        public string Department_Name { get; set; }
        public DateTime Contract_Date { get; set;}
        public DateTime Expiry_Date { get; set; }
        public DateTime Birthday { get; set; }
        public string Note { get; set; }
    }
}
