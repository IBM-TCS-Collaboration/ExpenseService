using ExpenseService.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseService.Database
{
    public class DatabaseContext: DbContext
    {
        public DbSet<expense> Expenses { get; set; }
        public DbSet<expenseAudit> ExpensesAudit { get; set; }

        public DbSet<documents> Documents { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(@"server=localhost; port=3306; database=expensedb; user=root; password=123456");
        }
    }
}
