﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OIDBMVCWEBSITE.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OIDBEntities : DbContext
    {
        public OIDBEntities()
            : base("name=OIDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Designation_Master> Designation_Master { get; set; }
        public DbSet<SiteUser> SiteUsers { get; set; }
        public DbSet<St_PermissionType> St_PermissionType { get; set; }
        public DbSet<st_components> st_components { get; set; }
        public DbSet<st_module> st_module { get; set; }
        public DbSet<map_component_usertype> map_component_usertype { get; set; }
        public DbSet<permission> permissions { get; set; }
        public DbSet<Failed_Login_Attempts> Failed_Login_Attempts { get; set; }
        public DbSet<LoginLogOffUserDetail> LoginLogOffUserDetails { get; set; }
        public DbSet<UserTrail> UserTrails { get; set; }
    }
}
