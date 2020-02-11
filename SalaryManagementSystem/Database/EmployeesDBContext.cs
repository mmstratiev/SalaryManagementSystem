using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Common;
using System.Configuration;

namespace SalaryManagementSystem
{
    class EmployeesDBContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeSalaryBill> EmployeeSalaryBills { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeSalaryBill>().HasRequired(bill => bill.Employee);
        }
    }
}
