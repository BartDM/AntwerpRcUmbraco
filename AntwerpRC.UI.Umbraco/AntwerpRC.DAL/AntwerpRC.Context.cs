﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AntwerpRCEntities : DbContext
    {
        public AntwerpRCEntities()
            : base("name=AntwerpRCEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Address> Address { get; set; }
        public DbSet<Calendar> Calendar { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<CategoryCalendar> CategoryCalendar { get; set; }
        public DbSet<Club> Club { get; set; }
        public DbSet<ClubAlias> ClubAlias { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Division> Division { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<Season> Season { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<TeamClub> TeamClub { get; set; }
        public DbSet<ScoreTable> ScoreTable { get; set; }
        public DbSet<ScoreTableLine> ScoreTableLine { get; set; }
    }
}