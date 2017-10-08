using LP.PRManagement.Common.Models.Domain;
using LP.PRManagement.Core.Managers.Interfaces;
using LP.PRManagement.Dal.Persistance;

namespace LP.PRManagement.Core.Managers
{
    public class ArtistManager : BaseManager<Artist>, IArtistManager
    {
        public ArtistManager(IGeneralUnitOfWork generalUnitOfWork) : base(generalUnitOfWork)
        {
        }

        protected override IRepository<Artist> Repository => GeneralUnitOfWork.Artist;
    }
}
