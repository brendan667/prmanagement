using LP.PRManagement.Common.Models.Domain;
using LP.PRManagement.Core.Managers.Interfaces;
using LP.PRManagement.Dal.Persistance;
using System.Threading.Tasks;

namespace LP.PRManagement.Core.Managers
{
    public class UserManager : BaseManager<User>, IUserManager
    {
        public UserManager(IGeneralUnitOfWork generalUnitOfWork)
           : base (generalUnitOfWork)
        {
        }
        
        protected override IRepository<User> Repository => GeneralUnitOfWork.User;

        public async Task<string> GetUser(int id)
        {
            var user = await Repository.FindOne(x => x.Email == "");
            return user.Email;
        }
    }
}
