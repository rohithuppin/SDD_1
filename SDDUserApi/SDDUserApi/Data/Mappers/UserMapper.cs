using AutoMapper;
using SDDUserApi.Data.DTO;
using SDDUserApi.Data.Model;

namespace SDDUserApi.Data.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper() 
        {
            CreateMap<User, UserDto>();

            CreateMap<UserDto, User>();
        }
    }
}
