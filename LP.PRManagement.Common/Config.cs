using System.Configuration;

namespace LP.PRManagement.Common
{
    public class Config : IConfig
    {
        public string Test => ConfigurationManager.AppSettings["Test"];
    }
}
