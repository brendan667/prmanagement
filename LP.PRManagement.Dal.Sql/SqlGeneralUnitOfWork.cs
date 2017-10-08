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
            : this("PRManagement")
        {

        }

        public SqlGeneralUnitOfWork(string connectionString)
        {
            string databaseName = SqlConnectionHelper.GetDatabaseNameFromUri(connectionString);
            Log.Info("Sql connect to " + connectionString);
            try
            {
                var context = new PRManagementContext(connectionString);
                SqlConfiguration.Instance().Update(context).Wait();
                InitializeRepositories(context);
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

        public IRepository<User> User { get; private set; }
        public IRepository<DbVersion> DbVersion { get; private set; }
        public IRepository<Province> Province { get; private set; }
        public IRepository<Venue> Venue { get; private set; }
        public IRepository<Festival> Festival { get; private set; }
        public IRepository<ApplyingType> ApplyingType { get; private set; }
        public IRepository<Artist> Artist { get; private set; }
        public IRepository<ArtistLink> ArtistLink { get; private set; }
        public IRepository<ArtistCalendar> ArtistCalendar { get; private set; }
        public IRepository<ContactInfoGroup> ContactInfoGroup { get; private set; }
        public IRepository<ContactInfo> ContactInfo { get; private set; }
        public IRepository<ContactInfoType> ContactInfoType { get; private set; }

        #endregion

        private void InitializeRepositories(PRManagementContext context)
        {
            SqlConfiguration.Instance().Update(context).Wait();
            User = new SqlRepository<User>(context);
            DbVersion = new SqlRepository<DbVersion>(context);
            Province = new SqlRepository<Province>(context);
            Venue = new SqlRepository<Venue>(context);
            Festival = new SqlRepository<Festival>(context);
            ApplyingType = new SqlRepository<ApplyingType>(context);
            Artist = new SqlRepository<Artist>(context);
            ArtistLink = new SqlRepository<ArtistLink>(context);
            ArtistCalendar = new SqlRepository<ArtistCalendar>(context);
            ContactInfoGroup = new SqlRepository<ContactInfoGroup>(context);
            ContactInfo = new SqlRepository<ContactInfo>(context);
            ContactInfoType = new SqlRepository<ContactInfoType>(context);
        }
    }
}
