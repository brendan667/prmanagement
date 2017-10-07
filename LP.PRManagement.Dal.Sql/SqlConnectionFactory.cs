using log4net;
using LP.PRManagement.Dal.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LP.PRManagement.Dal.Sql
{
    public class SqlConnectionFactory : IGeneralUnitOfWorkFactory
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly string _connectionString;
        private readonly string _databaseName;
        private readonly Lazy<IGeneralUnitOfWork> _singleConnection;

        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
            _databaseName = new Uri(_connectionString).Segments.Skip(1).FirstOrDefault() ?? "PRManagement";
            _singleConnection = new Lazy<IGeneralUnitOfWork>(GeneralUnitOfWork);
        }

        public static IGeneralUnitOfWork New
        {
            get { return new SqlConnectionFactory("").GetConnection(); }
        }

        public string DatabaseName
        {
            get { return _databaseName; }
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
            IMongoDatabase database = DatabaseOnly();
            Log.Info("Apply update");
            MongoConfiguration.Instance().Update(database).Wait();
            return new MongoGeneralUnitOfWork(database);
        }

        #endregion

        public IMongoDatabase DatabaseOnly()
        {
            IMongoClient client = ClientOnly();
            IMongoDatabase database = client.GetDatabase(_databaseName);
            return database;
        }

        #region Private Methods

        private IMongoClient ClientOnly()
        {
            return new MongoClient(_connectionString);
        }

        #endregion
    }
}
