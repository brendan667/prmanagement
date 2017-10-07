using LP.PRManagement.Common.Models.Domain.Base;

namespace LP.PRManagement.Common.Models.Domain
{
    public class User : BaseDalModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
