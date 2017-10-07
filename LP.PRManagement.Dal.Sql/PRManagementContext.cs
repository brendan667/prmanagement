using LP.PRManagement.Common.Models.Domain;
using System.Data.Entity;

namespace LP.PRManagement.Dal.Sql
{
    public class PRManagementContext : DbContext
    {
        public PRManagementContext(string connectionStringName)
            : base(connectionStringName)
        {}

        public DbSet<User> Users { get; set; }
    }
}
