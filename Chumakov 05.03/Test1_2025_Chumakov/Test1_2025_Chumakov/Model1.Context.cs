﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Test1_2025_Chumakov
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class user245_dbEntities : DbContext
    {
        public user245_dbEntities()
            : base("name=user245_dbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<region> region { get; set; }
        public virtual DbSet<roole> roole { get; set; }
        public virtual DbSet<Strani> Strani { get; set; }
        public virtual DbSet<zapisi> zapisi { get; set; }
    }
}
