using AutoMapper;
using SnapFile.Application.DTOs.UserDTOs;
using SnapFile.Domain.Entities;

namespace SnapFile.Application.Mapping
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.Position != null ? src.Position.Name : null))
                .ForMember(dest => dest.DepartmentName, opt=> opt.MapFrom(src=>src.Department != null ? src.Department.Name : null));
            
            CreateMap<User, ShortUserDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null));
        }
    }
}
