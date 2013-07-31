//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AntwerpRC.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Address
    {
        public Address()
        {
            this.CalenderItems = new HashSet<CalenderItems>();
            this.CalenderItems1 = new HashSet<CalenderItems>();
            this.TeamAddresses = new HashSet<TeamAddresses>();
        }
    
        public long AddressId { get; set; }
        public string Street { get; set; }
        public string Nr { get; set; }
        public string Postalcode { get; set; }
        public string City { get; set; }
        public Nullable<long> CountryId { get; set; }
        public bool AuditDeleted { get; set; }
        public int AuditCreatedBy { get; set; }
        public System.DateTime AuditCreatedOn { get; set; }
        public Nullable<int> AuditModifiedBy { get; set; }
        public Nullable<System.DateTime> AuditModifiedOn { get; set; }
    
        public virtual Countries Countries { get; set; }
        public virtual ICollection<CalenderItems> CalenderItems { get; set; }
        public virtual ICollection<CalenderItems> CalenderItems1 { get; set; }
        public virtual ICollection<TeamAddresses> TeamAddresses { get; set; }
    }
}
