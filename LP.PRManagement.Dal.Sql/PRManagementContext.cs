using LP.PRManagement.Common.Models.Domain;
using System.Data.Entity;

namespace LP.PRManagement.Dal.Sql
{
    public class PRManagementContext : DbContext
    {
        public PRManagementContext(string connectionStringName)
            : base(connectionStringName)
        {}

        public DbSet<User> User { get; set; }
        public DbSet<DbVersion> DbVersion { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<Venue> Venue { get; set; }

        public DbSet<Festival> Festival { get; set; }
        public DbSet<ApplyingType> ApplyingType { get; set; }

        public DbSet<Artist> Artist { get; set; }
        public DbSet<ArtistLink> ArtistLink { get; set; }
        public DbSet<ArtistCalendar> ArtistCalendar { get; set; }

        public DbSet<ContactInfoGroup> ContactInfoGroup { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }
        public DbSet<ContactInfoType> ContactInfoType { get; set; }
    }
}
