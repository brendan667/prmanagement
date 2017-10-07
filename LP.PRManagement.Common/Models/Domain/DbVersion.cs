using LP.PRManagement.Common.Models.Domain.Base;

namespace LP.PRManagement.Common.Models.Domain
{
    public class DbVersion : BaseDalModel
    {
        public int Version { get; set; }
        public string Name { get; set; }
    }
}
