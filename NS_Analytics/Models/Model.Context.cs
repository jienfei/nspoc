﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NS_Analytics.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class NS_AnalyticModelContainer : DbContext
    {
        public NS_AnalyticModelContainer()
            : base("name=NS_AnalyticModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Period> Period { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<UserPeriod> UserPeriod { get; set; }
        public virtual DbSet<AnswersResult> AnswersResult { get; set; }
    }
}
