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
    
    public partial class Club
    {
        public Club()
        {
            this.Address = new HashSet<Address>();
            this.ClubAlias = new HashSet<ClubAlias>();
            this.TeamClub = new HashSet<TeamClub>();
        }
    
        public long ClubId { get; set; }
        public string ClubName { get; set; }
        public string Colour { get; set; }
        public bool HomeClub { get; set; }
        public bool AuditDeleted { get; set; }
        public string AuditCreatedBy { get; set; }
        public System.DateTime AuditCreatedOn { get; set; }
        public string AuditModifiedBy { get; set; }
        public Nullable<System.DateTime> AuditModifiedOn { get; set; }
    
        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<ClubAlias> ClubAlias { get; set; }
        public virtual ICollection<TeamClub> TeamClub { get; set; }
    }
}