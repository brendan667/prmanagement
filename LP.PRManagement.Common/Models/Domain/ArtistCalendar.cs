using LP.PRManagement.Common.Models.Domain.Base;
using System;

namespace LP.PRManagement.Common.Models.Domain
{
    public class ArtistCalendar : BaseDalModel
    {
        public string TechInvolved { get; set; }
        public bool IsInvoiced { get; set; }
        public bool IsPaid { get; set; }
        public DateTime DateAndTime { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual Venue Venue { get; set; }
    }
}
