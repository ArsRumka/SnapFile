using AutoMapper;
using SnapFile.Application.DTOs.TemplateDTOs;
using SnapFile.Domain.Entities;

namespace SnapFile.Application.Mapping
{
    public class TemplateProfile : Profile
    {
        public TemplateProfile()
        {
            CreateMap<Template, TemplateDto>();
            CreateMap<TemplateCreateDto, Template>();
            CreateMap<TemplateUpdateDto, Template>();
        }
    }
}
