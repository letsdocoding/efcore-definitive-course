using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EfCoreCompleteCourse.TestConsole
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=(LocalDb)\MSSQLLocalDb;Initial Catalog=TestDb;Integrated Security=true;");
        }
    }
}
