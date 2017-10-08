using LP.PRManagement.Common.Models.Domain;
using LP.PRManagement.Core.Managers.Interfaces;
using LP.PRManagement.Dal.Persistance;

namespace LP.PRManagement.Core.Managers
{
    public class ContactInfoGroupManager : BaseManager<ContactInfoGroup>, IContactInfoGroupManager
    {
        public ContactInfoGroupManager(IGeneralUnitOfWork generalUnitOfWork) : base(generalUnitOfWork)
        {
        }

        protected override IRepository<ContactInfoGroup> Repository => GeneralUnitOfWork.ContactInfoGroup;
    }
}
