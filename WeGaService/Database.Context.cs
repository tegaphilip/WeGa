﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WeGaService
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WegaEntities : DbContext
    {
        public WegaEntities()
            : base("name=WegaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<game_words_history> game_words_history { get; set; }
        public DbSet<game> games { get; set; }
        public DbSet<player> players { get; set; }
        public DbSet<word> words { get; set; }
    }
}