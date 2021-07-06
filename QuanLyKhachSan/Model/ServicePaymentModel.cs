using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.Model
{
    public class ServicePaymentModel
    {
        public int ID { get; set; }
        public int STT { get; set; }
        public string ServiceName { get; set; }
        public int Useage { get; set; }
        public double UnitPrice { get; set; }
        public string Unit { get; set; }
        public double Sum { get; set; }
    }
}
