﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Byporten
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class byportenEntities : DbContext
    {
        public byportenEntities()
            : base("name=byportenEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<adminuser> adminuser { get; set; }
        public DbSet<aktuelt> aktuelt { get; set; }
        public DbSet<availablepositions> availablepositions { get; set; }
        public DbSet<butikker> butikker { get; set; }
        public DbSet<createpost> createpost { get; set; }
    }
}
