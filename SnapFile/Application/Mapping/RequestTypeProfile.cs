using AutoMapper;
using SnapFile.Application.DTOs.RequestTypeDTOs;
using SnapFile.Domain.Entities;

namespace SnapFile.Application.Mapping
{
    public class RequestTypeProfile : Profile
    {
        public RequestTypeProfile()
        {
            CreateMap<RequestType, RequestTypeDto>();
            CreateMap<RequestTypeCreateDto, RequestType>();
            CreateMap<RequestTypeUpdateDto, RequestType>();
        }
    }
}
