//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Repository.Application.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductMixing
    {
        public long ProductMixingId { get; set; }
        public string ProductMixingNumber { get; set; }
        public string Site { get; set; }
        public string BatchMixCode { get; set; }
        public string OutcomeProudct { get; set; }
        public Nullable<double> Ammount { get; set; }
        public Nullable<System.DateTime> MixingTime { get; set; }
        public string MixedBy { get; set; }
        public string Remark { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<short> IsActive { get; set; }
    
        public virtual BatchMix BatchMix { get; set; }
        public virtual PRODUCT PRODUCT { get; set; }
        public virtual SITE SITE1 { get; set; }
    }
}
