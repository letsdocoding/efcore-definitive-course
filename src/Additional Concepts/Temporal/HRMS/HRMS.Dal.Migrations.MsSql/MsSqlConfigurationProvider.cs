using HRMS.Dal.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Dal.Migrations.MsSql
{
    public class MsSqlConfigurationProvider : IDbSpecificConfigurationProvider
    {
        public void ConfigureDatabaseDependentExtensions(ModelBuilder modelBuilder)
        {
            /**********Step 1. ************/
            modelBuilder.Entity<User>().ToTable("Users", b => b.IsTemporal());

        }
    }
}
