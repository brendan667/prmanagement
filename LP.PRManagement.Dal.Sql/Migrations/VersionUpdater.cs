using log4net;
using LP.PRManagement.Common.Models.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace LP.PRManagement.Dal.Sql.Migrations
{
    public class VersionUpdater
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IMigration[] _updates;
        private readonly Mutex _resetEvent = new Mutex(false, @"PRManagement/VersionUpdater");

        public VersionUpdater(IMigration[] updates)
        {
            _updates = updates;
        }

        public Task Update(PRManagementContext context)
        {

            return Task.Run(() =>
            {
                if (_resetEvent.WaitOne(TimeSpan.FromSeconds(5)))
                {

                    var repository = new SqlRepository<DbVersion>(context);
                    List<DbVersion> versions = repository.Find().Result;
                    Log.Info(string.Format("Found {0} database updates in database and {1} in code", versions.Count, _updates.Length));
                    for (int i = 0; i < _updates.Length; i++)
                    {
                        Log.DebugFormat("Updating no: {0}", i);
                        IMigration migrateInitialize = _updates[i];
                        EnsureThatVersionDoesNotExistThenUpdate(versions, i, migrateInitialize, repository, context).Wait();
                    }
                    Log.Info("Done");

                    _resetEvent.ReleaseMutex();
                }
            });
        }

        #region Private Methods

        private async Task EnsureThatVersionDoesNotExistThenUpdate(IEnumerable<DbVersion> versions, int i, 
            IMigration migrateInitialize, SqlRepository<DbVersion> repository, PRManagementContext context)
        {
            DbVersion version = versions.FirstOrDefault(x => x.Version == i);
            if (version == null)
            {
                Log.Info(string.Format("Running version update {0}", migrateInitialize.GetType().Name));
                await RunTheUpdate(migrateInitialize, context);
                var dbVersion1 = new DbVersion { Version = i, Name = migrateInitialize.GetType().Name };
                await repository.Add(dbVersion1);
            }
        }

        private async Task RunTheUpdate(IMigration migrateInitialize, PRManagementContext context)
        {
            Log.Info(string.Format("Starting {0} db update", migrateInitialize.GetType().Name));
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await migrateInitialize.Update(context);
            stopwatch.Stop();
            Log.Info(string.Format("Done {0} in {1}ms", migrateInitialize.GetType().Name, stopwatch.ElapsedMilliseconds));
        }

        #endregion
    }
}
