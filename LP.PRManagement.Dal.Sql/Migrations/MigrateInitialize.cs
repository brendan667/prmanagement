using LP.PRManagement.Common.Models.Domain;
using System.Threading.Tasks;

namespace LP.PRManagement.Dal.Sql.Migrations
{
    public class MigrateInitialize : IMigration
    {
        private SqlRepository<User> _users;

        public async Task Update(PRManagementContext context)
        {
            InitializeRepos(context);
            await AddUser();
        }

        private void InitializeRepos(PRManagementContext context)
        {
            _users = new SqlRepository<User>(context);
        }

        private async Task AddUser()
        {
            var user = new User
            {
                Email = "test@test.com",
                Password = "test"
            };
            await _users.Add(user);
        }
    }
}
