using LP.PRManagement.Common.Models.Domain.Base;

namespace LP.PRManagement.Common.Models.Domain
{
    public class ContactInfo : BaseDalModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }

        public virtual ContactInfoGroup ContactInfoGroup { get; set; }
        public virtual ContactInfoType ContactInfoType { get; set; }
    }
}
