﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestWebAPI
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class iseeSCHOOLEntities : DbContext
    {
        public iseeSCHOOLEntities()
            : base("name=iseeSCHOOLEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CLASS> CLASSes { get; set; }
        public virtual DbSet<DOCUMENT> DOCUMENTS { get; set; }
        public virtual DbSet<EMPLOYEE_DETAILS> EMPLOYEE_DETAILS { get; set; }
        public virtual DbSet<STUDENT_MST> STUDENT_MST { get; set; }
    }
}