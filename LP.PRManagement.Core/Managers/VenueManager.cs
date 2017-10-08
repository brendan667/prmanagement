using LP.PRManagement.Common.Models.Domain;
using LP.PRManagement.Core.Managers.Interfaces;
using LP.PRManagement.Dal.Persistance;

namespace LP.PRManagement.Core.Managers
{
    public class VenueManager : BaseManager<Venue>, IVenueManager
    {
        public VenueManager(IGeneralUnitOfWork generalUnitOfWork) : base(generalUnitOfWork)
        {
        }

        protected override IRepository<Venue> Repository => GeneralUnitOfWork.Venue;
    }
}
