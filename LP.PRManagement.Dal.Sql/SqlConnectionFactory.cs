using log4net;
using LP.PRManagement.Dal.Persistance;
using System;
using System.Reflection;

namespace LP.PRManagement.Dal.Sql
{
    public class SqlConnectionFactory : IGeneralUnitOfWorkFactory
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly string _connectionString;
        private readonly Lazy<IGeneralUnitOfWork> _singleConnection;

        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
            _singleConnection = new Lazy<IGeneralUnitOfWork>(GeneralUnitOfWork);
        }
        
        public string ConnectionString
        {
            get { return _connectionString; }
        }

        #region IGeneralUnitOfWorkFactory Members

        public IGeneralUnitOfWork GetConnection()
        {
            return _singleConnection.Value;
        }

        private IGeneralUnitOfWork GeneralUnitOfWork()
        {
            Log.Info("Create new connection to " + _connectionString);
            PRManagementContext context = DatabaseOnly();
            Log.Info("Apply update");
            SqlConfiguration.Instance().Update(context).Wait();
            return new SqlGeneralUnitOfWork(context);
        }

        #endregion

        public PRManagementContext DatabaseOnly()
        {
            return new PRManagementContext(_connectionString);
        }
    }
}
