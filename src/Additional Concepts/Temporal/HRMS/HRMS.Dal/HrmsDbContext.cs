using System;
using HRMS.Dal.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Dal
{
    public class HrmsDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public HrmsDbContext(DbContextOptions<HrmsDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(b => b.OfficeName).HasDefaultValue(null);

            modelBuilder.Entity<UserRole>().HasData(new UserRole[]
            {
                new UserRole {UserRoleId = 1, RoleName = "user"},
                new UserRole {UserRoleId = 2, RoleName = "manager"}
            });
            modelBuilder.Entity<User>().HasData(new User[]
            {
                new User()
                {
                    UserId = 1, FirstName = "Vineet", LastName = "Yadav", CreatedBy = "system",
                    CreatedOn = new DateTimeOffset(2022, 05, 16, 0, 0, 0, TimeSpan.Zero), OfficeName = "DEL",
                    DateOfBirth = new DateTime(2000, 01, 01), ModifiedBy = "",
                    ModifiedOn = new DateTimeOffset(2022, 05, 16, 0, 0, 0, TimeSpan.Zero)
                }
            });
        }
    }
}
