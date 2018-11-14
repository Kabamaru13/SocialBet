using AutoMapper;
using SocialBet.Dtos;
using SocialBet.Models;

namespace SocialBet.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
