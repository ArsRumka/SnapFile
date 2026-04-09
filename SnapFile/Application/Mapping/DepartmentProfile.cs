using AutoMapper;
using SnapFile.Application.DTOs.DepartmentDTOs;
using SnapFile.Domain.Entities;

namespace SnapFile.Application.Mapping
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDto>()
                .ForMember(dest => dest.HeadName, opt => opt.MapFrom(src => src.Head != null ? src.Head.FullName : null));

            CreateMap<DepartmentCreateDto, Department>();
            CreateMap<DepartmentUpdateDto, Department>();
        }
    }
}
