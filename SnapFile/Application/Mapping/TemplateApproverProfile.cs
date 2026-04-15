using AutoMapper;
using SnapFile.Application.DTOs.TemplateApproverDTOs;
using SnapFile.Domain.Entities;

namespace SnapFile.Application.Mapping
{
    public class TemplateApproverProfile : Profile
    {
        public TemplateApproverProfile()
        {
            CreateMap<TemplateApprover, TemplateApproverDto>();
            CreateMap<TemplateApproverCreateDto, TemplateApprover>();
            CreateMap<TemplateApproverUpdateDto, TemplateApprover>();
        }
    }
}
