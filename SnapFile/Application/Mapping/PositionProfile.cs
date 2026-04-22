using AutoMapper;
using SnapFile.Application.DTOs.PositionDTOs;
using SnapFile.Application.DTOs.UserDTOs;
using SnapFile.Domain.Entities;

namespace SnapFile.Application.Mapping
{
    public class PositionProfile:Profile
    {
        public PositionProfile()
        {
            CreateMap<Position, PositionDto>();
            CreateMap<PositionDto, Position>();
            CreateMap<PositionCreateDto, Position>();

            CreateMap<UpdatePositionDto, Position>();

            CreateMap<Position, PositionWithUsersDto>()
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users
                .Select(u => new ShortUserDto
                {
                    FullName = u.FullName,
                    DepartmentName = u.Department != null ? u.Department.Name : null,
                    Phone = u.Phone
                }).ToList()));

            CreateMap<User, ShortUserDto>();
                
        }
    }
}
