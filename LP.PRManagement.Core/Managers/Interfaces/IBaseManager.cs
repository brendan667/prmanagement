using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LP.PRManagement.Core.Managers.Interfaces
{
    public interface IBaseManager<T>
    {
        Task<List<T>> GetAll();
        Task<T> Get(Guid refId);
        Task<T> Delete(Guid refId);
        Task<T> Update(T entity);
        Task<T> Insert(T entity);
    }
}
