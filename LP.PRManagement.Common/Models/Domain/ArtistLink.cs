using LP.PRManagement.Common.Models.Domain.Base;

namespace LP.PRManagement.Common.Models.Domain
{
    public class ArtistLink : BaseDalModel
    {
        public string Url { get; set; }

        public virtual Artist Artist { get; set; }
    }
}
