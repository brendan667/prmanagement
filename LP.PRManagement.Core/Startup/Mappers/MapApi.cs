using AutoMapper;
using LP.PRManagement.Common.Models.Domain;
using LP.PRManagement.Common.Models.DTOs.User;

namespace LP.PRManagement.Core.Startup.Mappers
{
    public static class MapApi
    {
        public static void MapUserModel()
        {
            Mapper.Initialize(x =>
            {
                x.CreateMap<User, UserResponseModel>();
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}