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
    
    public partial class TableSequence
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public Nullable<long> DeliveryOrder { get; set; }
        public Nullable<long> DeliveryOrderInvoice { get; set; }
        public Nullable<long> DeliveryRequest { get; set; }
        public Nullable<long> ProductMixing { get; set; }
    }
}