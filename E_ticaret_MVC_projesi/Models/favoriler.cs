//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace E_ticaret_MVC_projesi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class favoriler
    {
        public long favno { get; set; }
        public int uyeid { get; set; }
        public int urunid { get; set; }
    
        public virtual urunler urunler { get; set; }
        public virtual uyeler uyeler { get; set; }
    }
}
