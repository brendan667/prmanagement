using LP.PRManagement.Common.Models.Domain.Base;
using System.Collections.Generic;

namespace LP.PRManagement.Common.Models.Domain
{
    public class ApplyingType : BaseDalModel
    {
        public ApplyingType()
        {
            Festivals = new List<Festival>();
        }

        public string Name { get; set; }

        public virtual IList<Festival> Festivals { get; set; }
    }
}
