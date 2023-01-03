using AutoMapper;
using PhotoDocumentMicroService.Application.DTO;
using PhotoDocumentMicroService.Domain.Entities.Models;

namespace PhotoDocumentMicroService.Extensions;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Document, DocumentDto>()
            .ForMember(f => f.Id,
                t => t.MapFrom(r => r.RowKey));

        CreateMap<Document, DocumentForCreatedDto>().ReverseMap();
    }
}