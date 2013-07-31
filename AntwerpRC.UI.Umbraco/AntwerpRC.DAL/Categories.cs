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
    
    public partial class Categories
    {
        public Categories()
        {
            this.CalenderItems = new HashSet<CalenderItems>();
            this.CategoryTeams = new HashSet<CategoryTeams>();
        }
    
        public long CategoryId { get; set; }
        public string Description { get; set; }
        public Nullable<int> DefaultGameTime { get; set; }
        public Nullable<System.TimeSpan> DefaultStartTime { get; set; }
        public bool AuditDeleted { get; set; }
        public int AuditCreatedBy { get; set; }
        public System.DateTime AuditCreatedOn { get; set; }
        public Nullable<int> AuditModifiedBy { get; set; }
        public Nullable<System.DateTime> AuditModifiedOn { get; set; }
    
        public virtual ICollection<CalenderItems> CalenderItems { get; set; }
        public virtual ICollection<CategoryTeams> CategoryTeams { get; set; }
    }
}