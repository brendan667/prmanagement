using LP.PRManagement.Dal.Sql.Migrations;
using System.Threading.Tasks;

namespace LP.PRManagement.Dal.Sql
{
    public class SqlConfiguration
    {
        private static readonly object Locker = new object();
        private static SqlConfiguration _instance;
        private readonly IMigration[] _updates;
        private Task _update;

        protected SqlConfiguration()
        {
            _updates = new IMigration[] {
                new MigrateInitialize()
            };
        }

        public Task Update(PRManagementContext context)
        {
            lock (_instance)
            {
                if (_update == null)
                {
                    var versionUpdater = new VersionUpdater(_updates);
                    _update = versionUpdater.Update(context);
                }
            }
            return _update;
        }

        #region Instance

        public static SqlConfiguration Instance()
        {
            if (_instance == null)
                lock (Locker)
                {
                    if (_instance == null)
                    {
                        _instance = new SqlConfiguration();
                    }
                }
            return _instance;
        }

        #endregion
    }
}
