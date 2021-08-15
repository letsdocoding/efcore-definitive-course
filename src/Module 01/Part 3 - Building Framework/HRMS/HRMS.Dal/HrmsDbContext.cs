using HRMS.Dal.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Dal
{
    internal class HrmsDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
