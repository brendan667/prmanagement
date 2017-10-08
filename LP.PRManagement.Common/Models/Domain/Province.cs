using LP.PRManagement.Common.Models.Domain.Base;
using System.Collections.Generic;

namespace LP.PRManagement.Common.Models.Domain
{
    public class Province : BaseDalModel
    {
        public Province()
        {
            Venues = new List<Venue>();
        }

        public string Name { get; set; }

        public virtual IList<Venue> Venues { get; set; }
    }
}
