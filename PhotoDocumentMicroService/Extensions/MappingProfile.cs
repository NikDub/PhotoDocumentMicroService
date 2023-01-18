using AutoMapper;
using PhotoDocumentMicroService.Application.Dto;
using PhotoDocumentMicroService.Domain.Entities.Models;

namespace PhotoDocumentMicroService.Extensions;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Document, DocumentDto>()
            .ForMember(documentDto => documentDto.Id,
                expression => expression.MapFrom(document => document.RowKey));
        CreateMap<Document, PhotoDto>()
            .ForMember(photoDto => photoDto.Id,
                expression => expression.MapFrom(document => document.RowKey));

        CreateMap<PhotoForCreatedDto, Document>().ReverseMap();
        CreateMap<DocumentForCreatedDto, Document>().ReverseMap();
    }
}