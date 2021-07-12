using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace QuanLyKhachSan.Model
{
    public class RoomModel
    {
        public Room room { get; set; }
        public string room_name { get; set; }
        public string room_type { get; set; }
        public string room_status { get; set; }
        public string room_price { get; set; }
        public string status { get; set; }
        public Brush color { get; set; }
    }
}
