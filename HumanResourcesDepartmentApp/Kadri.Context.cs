﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HumanResourcesDepartmentApp
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HumanResourcesDepartmentEntities1 : DbContext
    {
        public HumanResourcesDepartmentEntities1()
            : base("name=HumanResourcesDepartmentEntities1")
        {
        }
        private static HumanResourcesDepartmentEntities1 _context;
        public static HumanResourcesDepartmentEntities1 GetContext()
        {
            if (_context == null)
                _context = new HumanResourcesDepartmentEntities1();

            return _context;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<DayOfTheWeek> DayOfTheWeek { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<Staffing> Staffing { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Vacation_Schedule> Vacation_Schedule { get; set; }
        public virtual DbSet<Worker> Worker { get; set; }
    }
}