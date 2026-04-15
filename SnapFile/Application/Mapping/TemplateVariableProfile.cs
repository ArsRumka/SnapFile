using AutoMapper;
using SnapFile.Application.DTOs.TemplateVariableDTOs;
using SnapFile.Domain.Entities;

namespace SnapFile.Application.Mapping
{
    public class TemplateVariableProfile : Profile
    {
        public TemplateVariableProfile()
        {
            CreateMap<TemplateVariable, TemplateVariableDto>();
            CreateMap<TemplateVariableCreateDto, TemplateVariable>();
            CreateMap<TemplateVariableUpdateDto, TemplateVariable>();
        }
    }
}
