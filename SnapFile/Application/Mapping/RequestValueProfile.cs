using AutoMapper;
using SnapFile.Application.DTOs.RequestValueDTOs;
using SnapFile.Domain.Entities;

namespace SnapFile.Application.Mapping
{
    public class RequestValueProfile : Profile
    {
        public RequestValueProfile()
        {
            CreateMap<RequestValue, RequestValueDto>();
            CreateMap<RequestValueCreateDto, RequestValue>();
            CreateMap<RequestValueUpdateDto, RequestValue>();
        }
    }
}
