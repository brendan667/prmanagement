using log4net;
using LP.PRManagement.Common.Models.Domain;
using LP.PRManagement.Dal.Persistance;
using System;
using System.Reflection;

namespace LP.PRManagement.Dal.Sql
{
    public class SqlGeneralUnitOfWork : IGeneralUnitOfWork
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public SqlGeneralUnitOfWork(PRManagementContext context)
        {
            InitializeRepositories(context);
        }

        public SqlGeneralUnitOfWork()
            : this("")
        {

        }

        public SqlGeneralUnitOfWork(string connectionString)
        {
            string databaseName = MongoDbConnectionHelper.GetDatabaseNameFromUri(connectionString);
            Log.Info("Mongo connect to " + connectionString);
            try
            {
                var client = new MongoClient(connectionString);
                var mongoDatabase = client.GetDatabase(databaseName);
                SqlConfiguration.Instance().Update(mongoDatabase).Wait();
                InitializeRepositories(mongoDatabase);
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message, exception);
                throw;
            }
        }

        #region Implementation of IDisposable

        public void Dispose()
        {

        }

        #endregion

        #region Implementation of IGeneralUnitOfWork

        public IRepository<User> Users { get; private set; }
        //public IRepository<Role> Roles { get; private set; }
        //public IRepository<Company> Companies { get; private set; }
        //public IRepository<Trigger> Triggers { get; private set; }
        //public IRepository<TriggerState> TriggerStates { get; private set; }
        //public IRepository<TriggerAuditLog> TriggerAuditLog { get; private set; }
        //public IRepository<FunctionalGroup> FunctionalGroups { get; private set; }
        //public IRepository<Mandate> Mandates { get; private set; }
        //public IRepository<MandateAuditLog> MandateAuditLog { get; private set; }
        //public IRepository<MandateStatus> MandateStatus { get; private set; }

        #endregion

        private void InitializeRepositories(PRManagementContext context)
        {
            SqlConfiguration.Instance().Update(context).Wait();
            Users = new SqlRepository<User>(context);
            //Roles = new MongoRepository<Role>(database);
            //Companies = new MongoRepository<Company>(database);
            //Triggers = new MongoRepository<Trigger>(database);
            //TriggerStates = new MongoRepository<TriggerState>(database);
            //TriggerAuditLog = new MongoRepository<TriggerAuditLog>(database);
            //FunctionalGroups = new MongoRepository<FunctionalGroup>(database);
            //Mandates = new MongoRepository<Mandate>(database);
            //MandateAuditLog = new MongoRepository<MandateAuditLog>(database);
            //MandateStatus = new MongoRepository<MandateStatus>(database);
        }
    }
}
