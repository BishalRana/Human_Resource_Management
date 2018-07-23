using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Human_Resource_Management.Models
{
    public class ManagementContext :DbContext
    {
       
        public ManagementContext(DbContextOptions<ManagementContext> options) : base(options)
        {            
        }

        public DbSet<Company> Company { get; set; }
        public DbSet<SubCompany> SubCompany  { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Phone> Phone { get; set; }
        public DbSet<Address> Address { get; set; }

             
        //setting the relationship between entitites
        // setting referential acton on delete , on setnull the foreign key will be set to null and on cascade the prinicpal data and its dependend will be deleted.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubCompany>().HasMany(s => s.Employees).WithOne(e => e.SubCompany).IsRequired();
            modelBuilder.Entity<SubCompany>().HasMany(s => s.Employees).WithOne(e => e.SubCompany).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Company>().HasMany(s => s.SubCompanies).WithOne(e => e.Company).IsRequired();
            modelBuilder.Entity<Company>().HasMany(s => s.SubCompanies).WithOne(e => e.Company).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>().HasMany(a => a.Addresses).WithOne(e => e.Employee).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Employee>().HasMany(p => p.Phones).WithOne(e => e.Employee).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Employee>().HasMany(empPjt => empPjt.EmployeeProjects).WithOne(e => e.employee).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Position>().HasMany(p => p.Employees).WithOne(e => e.Positions).IsRequired();
            modelBuilder.Entity<Position>().HasMany(p => p.Employees).WithOne(e => e.Positions).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<SubCompany>().HasMany(p => p.Projects).WithOne(s => s.SubCompany).IsRequired();
            modelBuilder.Entity<SubCompany>().HasMany(p => p.Projects).WithOne(s => s.SubCompany).OnDelete(DeleteBehavior.Cascade);
                       
            modelBuilder.Entity<EmployeeProject>().HasKey(t => new { t._EmpId, t._PjtId });

            modelBuilder.Entity<Project>().HasMany(ep => ep.EmployeeProjects).WithOne(p => p.Project).OnDelete(DeleteBehavior.Cascade);


        }
    }
}
