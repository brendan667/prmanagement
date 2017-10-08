using LP.PRManagement.Common.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using LP.PRManagement.Common;

namespace LP.PRManagement.Dal.Sql.Migrations
{
    public class MigrateInitialize : IMigration
    {
        private SqlRepository<User> _users;
        private SqlRepository<Province> _provinces;
        private SqlRepository<ApplyingType> _applyingType;
        private SqlRepository<ContactInfoType> _contactInfoType;

        public async Task Update(PRManagementContext context)
        {
            InitializeRepos(context);
            await AddUser();
            await AddStaticProvinces();
            await AddStaticApplyingTypes();
            await AddStaticContactInfoTypes();
        }

        
        private void InitializeRepos(PRManagementContext context)
        {
            _users = new SqlRepository<User>(context);
            _provinces = new SqlRepository<Province>(context);
            _applyingType = new SqlRepository<ApplyingType>(context);
            _contactInfoType = new SqlRepository<ContactInfoType>(context);
        }


        #region Table Adds

        private async Task AddStaticApplyingTypes()
        {
            await _applyingType.AddRange(
                new List<ApplyingType>
                {
                    new ApplyingType
                    {
                        Name = "Online"
                    },
                    new ApplyingType
                    {
                        Name = "Email"
                    }
                });
        }

        private async Task AddUser()
        {
            await _users.Add(new User
            {
                Name = "Louise Pieterse",
                Email = "test@test.com",
                Password = PasswordHash.CreateHash("P@ssword123")
            });
        }

        private async Task AddStaticProvinces()
        {
            await _provinces.AddRange(new List<Province>
            {
                new Province
                {
                    Name = "Northern Cape"
                },
                new Province
                {
                    Name = "Eastern Cape"
                },
                new Province
                {
                    Name = "Free State"
                },
                new Province
                {
                    Name = "Western Cape"
                },
                new Province
                {
                    Name = "Limpopo"
                },
                new Province
                {
                    Name = "North West"
                },
                new Province
                {
                    Name = "KwaZulu-Natal"
                },
                new Province
                {
                    Name = "Mpumalanga"
                },
                new Province
                {
                    Name = "Gauteng"
                },
            });
        }

        private async Task AddStaticContactInfoTypes()
        {
            await _contactInfoType.AddRange(new List<ContactInfoType> {
                new ContactInfoType
{
                    Name = "Print"
                },
                new ContactInfoType
                {
                    Name = "Web"
                },
                new ContactInfoType
                {
                    Name = "Radio"
                },
                new ContactInfoType
                {
                    Name = "Artist"
                },
                new ContactInfoType
                {
                    Name = "Photographer"
                },
                new ContactInfoType
                {
                    Name = "Videographer"
                },
                new ContactInfoType
                {
                    Name = "Other"
                }
            });
        }

        #endregion


    }
}
