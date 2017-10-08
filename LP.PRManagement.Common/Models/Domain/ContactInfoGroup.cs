using LP.PRManagement.Common.Models.Domain.Base;
using System.Collections.Generic;

namespace LP.PRManagement.Common.Models.Domain
{
    public class ContactInfoGroup : BaseDalModel
    {
        public string Publication { get; set; }

        public virtual IList<ContactInfo> ContactInfos { get; set; }
    }
}
