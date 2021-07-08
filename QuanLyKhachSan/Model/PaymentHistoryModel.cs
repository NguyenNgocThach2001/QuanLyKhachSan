using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.Model
{
    public class PaymentHistoryModel
    {
        public Reservation reservation { get; set; }
        public string GuestName { get; set; }
        public string RoomName { get; set; }
        public string Amount { get; set; }
    }
}
