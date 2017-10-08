using LP.PRManagement.Common.Models.Domain.Base;
using System.Collections.Generic;

namespace LP.PRManagement.Common.Models.Domain
{
    public class Artist : BaseDalModel
    {
        public Artist()
        {
            ArtistLinks = new List<ArtistLink>();
            ArtistCalendars = new List<ArtistCalendar>();
        }

        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }

        public virtual IList<ArtistLink> ArtistLinks { get; private set; }
        public virtual IList<ArtistCalendar> ArtistCalendars { get; private set; }
    }
}
