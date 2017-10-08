using System;
using System.Linq;

namespace LP.PRManagement.Dal.Sql
{
    public class SqlConnectionHelper
    {
        public static string GetDatabaseNameFromUri(string connectionString)
        {
            return new Uri(connectionString).Segments.Skip(1).FirstOrDefault();
        }
    }
}
