//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyKhachSan.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reservation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reservation()
        {
            this.PaymentServices = new HashSet<PaymentService>();
        }
    
        public int Reservation_id { get; set; }
        public Nullable<int> guest_id { get; set; }
        public Nullable<int> room_id { get; set; }
        public Nullable<System.DateTime> reservation_date { get; set; }
        public Nullable<System.DateTime> check_in_date { get; set; }
        public Nullable<System.DateTime> check_out_date { get; set; }
        public Nullable<int> adult { get; set; }
        public Nullable<int> children { get; set; }
        public Nullable<double> amount { get; set; }
        public Nullable<System.Guid> Room_Service { get; set; }
        public Nullable<int> paid { get; set; }
    
        public virtual Guest Guest { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaymentService> PaymentServices { get; set; }
        public virtual Room Room { get; set; }
        public virtual RoomService RoomService { get; set; }
    }
}
