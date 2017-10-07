using System.Threading.Tasks;

namespace LP.PRManagement.Dal.Sql.Migrations
{
    public interface IMigration
    {
        Task Update(PRManagementContext context);
    }
}
