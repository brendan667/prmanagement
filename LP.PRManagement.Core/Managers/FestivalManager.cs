using LP.PRManagement.Common.Models.Domain;
using LP.PRManagement.Core.Managers.Interfaces;
using LP.PRManagement.Dal.Persistance;

namespace LP.PRManagement.Core.Managers
{
    public class FestivalManager : BaseManager<Festival>, IFestivalManager
    {
        public FestivalManager(IGeneralUnitOfWork generalUnitOfWork) : base(generalUnitOfWork)
        {
        }

        protected override IRepository<Festival> Repository => GeneralUnitOfWork.Festival;
    }
}
