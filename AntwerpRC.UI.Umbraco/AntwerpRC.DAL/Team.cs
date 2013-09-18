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
    
    public partial class Team
    {
        public Team()
        {
            this.TeamClub = new HashSet<TeamClub>();
            this.ScoreTable = new HashSet<ScoreTable>();
        }
    
        public long TeamId { get; set; }
        public long CategoryId { get; set; }
        public long SeasonId { get; set; }
        public Nullable<long> DivisionId { get; set; }
        public bool AuditDeleted { get; set; }
        public string AuditCreatedBy { get; set; }
        public System.DateTime AuditCreatedOn { get; set; }
        public string AuditModifiedBy { get; set; }
        public Nullable<System.DateTime> AuditModifiedOn { get; set; }
        public string CompetitionUrl { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Division Division { get; set; }
        public virtual Season Season { get; set; }
        public virtual ICollection<TeamClub> TeamClub { get; set; }
        public virtual ICollection<ScoreTable> ScoreTable { get; set; }
    }
}
