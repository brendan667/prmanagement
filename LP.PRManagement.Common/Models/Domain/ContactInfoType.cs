using LP.PRManagement.Common.Models.Domain.Base;
using System.Collections.Generic;

namespace LP.PRManagement.Common.Models.Domain
{
    public class ContactInfoType : BaseDalModel
    {
        public string Name { get; set; }

        public virtual IList<ContactInfo> ContactInfos { get; set; }
    }
}
