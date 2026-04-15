using AutoMapper;
using SnapFile.Application.DTOs.FormulationDTOs;
using SnapFile.Domain.Entities;

namespace SnapFile.Application.Mapping
{
    public class FormulationProfile : Profile
    {
        public FormulationProfile()
        {
            CreateMap<Formulation, FormulationDto>();
            CreateMap<FormulationCreateDto, Formulation>();
            CreateMap<FormulationUpdateDto, Formulation>();
        }
    }
}
