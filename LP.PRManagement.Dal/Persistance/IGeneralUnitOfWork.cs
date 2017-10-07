using LP.PRManagement.Common.Models.Domain;
using System;

namespace LP.PRManagement.Dal.Persistance
{
    public interface IGeneralUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        //IRepository<Role> Roles { get; }
        //IRepository<Company> Companies { get; }
        //IRepository<Trigger> Triggers { get; }
        //IRepository<TriggerState> TriggerStates { get; }
        //IRepository<TriggerAuditLog> TriggerAuditLog { get; }
        //IRepository<FunctionalGroup> FunctionalGroups { get; }
        //IRepository<Mandate> Mandates { get; }
        //IRepository<MandateAuditLog> MandateAuditLog { get; }
        //IRepository<MandateStatus> MandateStatus { get; }
    }
}
