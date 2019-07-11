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
    
    public partial class CLIENT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CLIENT()
        {
            this.ClientNostroBank = new HashSet<ClientNostroBank>();
            this.CONTRACT = new HashSet<CONTRACT>();
            this.UserClientMapping = new HashSet<UserClientMapping>();
        }
    
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string ClientType { get; set; }
        public string ClientCompanyName { get; set; }
        public string ClientCompanyPIC { get; set; }
        public string ClientCompanyPICPhoneNumber { get; set; }
        public string ClientCompanyLeaderEmailAddress { get; set; }
        public string ClientNPWP { get; set; }
        public string ClientPhoneNumber { get; set; }
        public string ClientEmail { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<short> IsActive { get; set; }
        public string Remark { get; set; }
        public string ClientCompanyPIC2 { get; set; }
        public string ClientCompanyPICPhoneNumber2 { get; set; }
        public string ClientCompanyLeaderEmailAddress2 { get; set; }
        public string ClientCompanyPIC3 { get; set; }
        public string ClientCompanyPICPhoneNumber3 { get; set; }
        public string ClientCompanyLeaderEmailAddress3 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientNostroBank> ClientNostroBank { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTRACT> CONTRACT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserClientMapping> UserClientMapping { get; set; }
    }
}
