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
    
    public partial class BatchProduct
    {
        public string BatchCode { get; set; }
        public string ProductId { get; set; }
        public Nullable<double> ProductPercentage { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<short> IsActive { get; set; }
    
        public virtual BATCH BATCH { get; set; }
        public virtual PRODUCT PRODUCT { get; set; }
    }
}
