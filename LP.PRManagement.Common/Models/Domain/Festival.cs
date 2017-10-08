using LP.PRManagement.Common.Models.Domain.Base;

namespace LP.PRManagement.Common.Models.Domain
{
    public class Festival : BaseDalModel
    {
        public string Name { get; set; }
        public string Area { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }

        public virtual ApplyingType ApplyingType { get; set; }
    }
}
