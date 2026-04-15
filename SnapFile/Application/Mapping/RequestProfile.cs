using AutoMapper;
using SnapFile.Application.DTOs.RequestDTOs;
using SnapFile.Domain.Entities;

namespace SnapFile.Application.Mapping
{
    public class RequestProfile : Profile
    {
        public RequestProfile()
        {
            CreateMap<Request, RequestDto>();
            CreateMap<RequestCreateDto, Request>();
            CreateMap<RequestUpdateDto, Request>();
        }
    }
}
