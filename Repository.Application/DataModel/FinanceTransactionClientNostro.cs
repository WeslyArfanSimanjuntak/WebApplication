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
    
    public partial class FinanceTransactionClientNostro
    {
        public string ClientNostroBankId { get; set; }
        public int Client { get; set; }
        public long FinanceTransactionId { get; set; }
        public string Remark { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<short> IsActive { get; set; }
    
        public virtual ClientNostroBank ClientNostroBank { get; set; }
        public virtual FinanceTransaction FinanceTransaction { get; set; }
    }
}
