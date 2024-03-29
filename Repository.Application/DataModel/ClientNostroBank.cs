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
    
    public partial class ClientNostroBank
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClientNostroBank()
        {
            this.FinanceTransactionClientNostro = new HashSet<FinanceTransactionClientNostro>();
        }
    
        public string ClientNostroBankId { get; set; }
        public int Client { get; set; }
        public string BankId { get; set; }
        public string BankBranchCode { get; set; }
        public string NostroBankName { get; set; }
        public string NostroAccountNumber { get; set; }
        public string NostroAccountName { get; set; }
        public string BankCategory { get; set; }
        public string Remark { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<short> IsActive { get; set; }
    
        public virtual CLIENT CLIENT1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FinanceTransactionClientNostro> FinanceTransactionClientNostro { get; set; }
    }
}
