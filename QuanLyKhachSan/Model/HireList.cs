using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKhachSan.Model
{
    public class HireModel
    {
        public Reservation reservation { get; set; }
        public string room_name { get; set; }
        public string guest_name { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime CheckoutDate { get; set; }
    }
}
