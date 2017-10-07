using log4net;
using LP.PRManagement.Common.Models.Domain.Interface;
using LP.PRManagement.Dal.Persistance;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace LP.PRManagement.Dal.Sql
{
    class SqlRepository<T> : IRepository<T> where T : class, IBaseDalModel
    {
        private readonly IDbSet<T> _dbSet;
        private readonly PRManagementContext _context;
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public SqlRepository(PRManagementContext context)
        {
            _dbSet = context.Set<T>();
            _context = context;
        }

        public IDbSet<T> DbSet => _dbSet;

        #region Implementation of IRepository<T>

        public IQueryable<T> Query() => _dbSet.AsQueryable();
        
        public async Task<T> Add(T entity)
        {
            try
            {
                Log.DebugFormat("Entering Add, entity typeof: {0}", typeof(T));
                entity.CreatedDate = DateTime.Now;
                Log.DebugFormat("About to insert one async");
                _dbSet.Add(entity);
                Log.DebugFormat("Finished inserting one async");
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        public async Task<IEnumerable<T>> AddRange(IEnumerable<T> entities)
        {
            IList<T> enumerable = entities as IList<T> ?? entities.ToList();
            foreach (T entity in enumerable)
            {
                entity.CreatedDate = DateTime.Now;
                _dbSet.Add(entity);
            }
            await _context.SaveChangesAsync();

            return enumerable;
        }

        public async Task<T> Update(Expression<Func<T, bool>> filter, T entity)
        {
            var gettingEntity = await _dbSet.SingleOrDefaultAsync(filter);

            if (gettingEntity == null)
            {
                throw new Exception("Could not find existing entity!");
            }

            _context.Entry(gettingEntity).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync();
            return entity;
        }
        
        public async Task<bool> Remove(Expression<Func<T, bool>> filter)
        {
            var foundEntity = await _dbSet.SingleOrDefaultAsync(filter);
            if (foundEntity == null)
            {
                throw new Exception("Could not find existing entity!");
            }

            _dbSet.Remove(foundEntity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<T>> Find(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.Where(filter).ToListAsync();
        }

        public async Task<T> FindOne(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.SingleOrDefaultAsync(filter);
        }

        public async Task<long> Count()
        {
            return await Count(x => true);
        }

        public async Task<long> Count(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.CountAsync(filter);
        }

        public async Task<List<T>> Find()
        {
            return await Find(x => true);
        }

        #endregion
    }
}
