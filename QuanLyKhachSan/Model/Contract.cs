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
    
    public partial class Contract
    {
        public int Contract_ID { get; set; }
        public Nullable<int> Staff_ID { get; set; }
        public string Context { get; set; }
    
        public virtual Staff Staff { get; set; }
        public virtual Staff Staff1 { get; set; }
    }
}
