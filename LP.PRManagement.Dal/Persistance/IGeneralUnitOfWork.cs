using LP.PRManagement.Common.Models.Domain;
using System;

namespace LP.PRManagement.Dal.Persistance
{
    public interface IGeneralUnitOfWork : IDisposable
    {
        IRepository<User> User { get; }
        IRepository<DbVersion> DbVersion { get; }
        IRepository<Province> Province { get; }
        IRepository<Venue> Venue { get; }
        IRepository<Festival> Festival { get; }
        IRepository<ApplyingType> ApplyingType { get; }
        IRepository<Artist> Artist { get; }
        IRepository<ArtistLink> ArtistLink { get; }
        IRepository<ArtistCalendar> ArtistCalendar { get; }
        IRepository<ContactInfoGroup> ContactInfoGroup { get; }
        IRepository<ContactInfo> ContactInfo { get; }
        IRepository<ContactInfoType> ContactInfoType { get; }
    }               
}
