using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.Model
{
    public class HireModel
    {
        public Reservation reservation { get; set; }
        public string room_name { get; set; }
        public string guest_name { get; set; }
        public string CheckinDate { get; set; }
        public string CheckoutDate { get; set; }

    }
}
