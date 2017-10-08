using LP.PRManagement.Common.Models.Domain.Base;

namespace LP.PRManagement.Common.Models.Domain
{
    public class Venue : BaseDalModel
    {
        public string Name { get; set; }
        public string Town { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string ContactPerson { get; set; }

        public virtual Province Province { get; set; }
    }
}
