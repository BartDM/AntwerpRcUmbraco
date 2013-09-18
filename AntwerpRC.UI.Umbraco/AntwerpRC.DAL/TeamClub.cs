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
    
    public partial class TeamClub
    {
        public TeamClub()
        {
            this.Game = new HashSet<Game>();
            this.Game1 = new HashSet<Game>();
            this.ScoreTableLine = new HashSet<ScoreTableLine>();
        }
    
        public long TeamClubId { get; set; }
        public long TeamId { get; set; }
        public long ClubId { get; set; }
        public bool AuditDeleted { get; set; }
        public string AuditCreatedBy { get; set; }
        public System.DateTime AuditCreatedOn { get; set; }
        public string AuditModifiedBy { get; set; }
        public Nullable<System.DateTime> AuditModifiedOn { get; set; }
        public string TeamName { get; set; }
    
        public virtual Club Club { get; set; }
        public virtual ICollection<Game> Game { get; set; }
        public virtual ICollection<Game> Game1 { get; set; }
        public virtual Team Team { get; set; }
        public virtual ICollection<ScoreTableLine> ScoreTableLine { get; set; }
    }
}