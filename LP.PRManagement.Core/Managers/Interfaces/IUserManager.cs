using LP.PRManagement.Common.Models.Domain;
using System.Threading.Tasks;

namespace LP.PRManagement.Core.Managers.Interfaces
{
    public interface IUserManager : IBaseManager<User>
    {
        Task<string> GetUser(int id);
    }
}
