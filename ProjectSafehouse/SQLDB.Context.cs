﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectSafehouse
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ProjectArsenalEntities : DbContext
    {
        public ProjectArsenalEntities()
            : base("name=ProjectArsenalEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<SQLUser> SQLUsers { get; set; }
        public DbSet<SQLCompany> SQLCompanies { get; set; }
        public DbSet<SQLProject> SQLProjects { get; set; }
        public DbSet<SQLRelease> SQLReleases { get; set; }
        public DbSet<SQLActionItem> SQLActionItems { get; set; }
        public DbSet<SQLActionItemType> SQLActionItemTypes { get; set; }
        public DbSet<SQLStatus> SQLStatuses { get; set; }
        public DbSet<SQLActionItemUser> SQLActionItemUsers { get; set; }
        public DbSet<SQLPriority> SQLPriorities { get; set; }
        public DbSet<SQLCompanyUser> SQLCompanyUsers { get; set; }
        public DbSet<SQLSystemRole> SQLSystemRoles { get; set; }
        public DbSet<SQLActionItemHistory> SQLActionItemHistories { get; set; }
    }
}
