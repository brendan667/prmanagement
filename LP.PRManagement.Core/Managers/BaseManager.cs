using log4net;
using LP.PRManagement.Common.Models.Domain.Base;
using LP.PRManagement.Core.Managers.Interfaces;
using LP.PRManagement.Dal.Persistance;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace LP.PRManagement.Core.Managers
{
    public abstract class BaseManager<T> : IBaseManager<T> where T : BaseDalModel
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IGeneralUnitOfWork _generalUnitOfWork;

        public ILog Log => _log;

        public IGeneralUnitOfWork GeneralUnitOfWork => _generalUnitOfWork;

        public BaseManager(IGeneralUnitOfWork generalUnitOfWork)
        {
            _generalUnitOfWork = generalUnitOfWork;
        }

        protected abstract IRepository<T> Repository { get; }

        public async Task<T> Delete(int id)
        {
            T entity = await Get(id);
            if(entity != null)
            {
                _log.Info($"Removing entity with id: {id}");
                await Repository.Remove(x => x.Id == id);
            }
            return entity;
        }

        public async Task<T> Get(int id)
        {
            return await Repository.FindOne(x => x.Id == id);
        }

        public async Task<List<T>> GetAll()
        {
            return await Repository.Find(x => true);
        }

        public async Task<T> Insert(T entity)
        {
            _log.Info($"Adding entity type: {typeof(T).Name}");
            return await Repository.Add(entity);
        }

        public async Task<T> Update(T entity)
        {
            _log.Info($"Updating entity type: {typeof(T).Name}");
            return await Repository.Update(x => x.Id == entity.Id, entity);
        }
    }
}
