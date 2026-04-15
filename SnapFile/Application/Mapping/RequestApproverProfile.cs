using AutoMapper;
using SnapFile.Application.DTOs.RequestApproverDTOs;
using SnapFile.Domain.Entities;

namespace SnapFile.Application.Mapping
{
    public class RequestApproverProfile : Profile
    {
        public RequestApproverProfile()
        {
            CreateMap<RequestApprover, RequestApproverDto>();
            CreateMap<RequestApproverCreateDto, RequestApprover>();
            CreateMap<RequestApproverUpdateDto, RequestApprover>();
        }
    }
}
