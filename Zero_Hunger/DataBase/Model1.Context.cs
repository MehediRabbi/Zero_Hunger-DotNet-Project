﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Zero_Hunger.DataBase
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Zero_HungerEntities1 : DbContext
    {
        public Zero_HungerEntities1()
            : base("name=Zero_HungerEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdminStaffInfo> AdminStaffInfoes { get; set; }
        public virtual DbSet<LogIninfo> LogIninfoes { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<RestuarentInfo> RestuarentInfoes { get; set; }
    }
}