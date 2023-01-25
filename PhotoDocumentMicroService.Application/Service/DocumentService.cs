using AutoMapper;
using PhotoDocumentMicroService.Application.Dto;
using PhotoDocumentMicroService.Application.Service.Abstractions;
using PhotoDocumentMicroService.Domain.Entities.Enums;
using PhotoDocumentMicroService.Domain.Entities.Models;
using PhotoDocumentMicroService.Infrastructure.Repository.Abstractions;

namespace PhotoDocumentMicroService.Application.Service;

public class DocumentService : IDocumentService
{
    private readonly IBlobRepository _blobRepository;
    private readonly IDocumentRepository _documentRepository;
    private readonly IMapper _mapper;

    public DocumentService(IDocumentRepository documentRepository, IBlobRepository blobRepository, IMapper mapper)
    {
        _documentRepository = documentRepository;
        _blobRepository = blobRepository;
        _mapper = mapper;
    }

    public async Task<PhotoDto> GetByIdAsync(Guid id)
    {
        var doc = await _documentRepository.GetEntityAsync(DocumentType.Photo.ToString("D"), id.ToString());
        var blobDoc = _blobRepository.DownloadAsync(doc.FileName);

        var docDto = _mapper.Map<PhotoDto>(doc);
        docDto.Value = (await blobDoc).ToArray();
        return docDto;
    }

    public async Task<List<DocumentDto>> GetByResultIdAsync(Guid resultId)
    {
        var doc = _documentRepository.GetEntityByResultId(resultId);

        var docDto = _mapper.Map<List<DocumentDto>>(doc);
        foreach (var item in docDto) item.Value = (await _blobRepository.DownloadAsync(item.FileName)).ToArray();
        return docDto;
    }

    public async Task<PhotoDto> CreatePhotoAsync(PhotoForCreatedDto model)
    {
        var doc = _mapper.Map<Document>(model);

        doc.FileName = await _blobRepository.UploadAsync(new MemoryStream(model.Value), model.FileName);
        doc.RowKey = Guid.NewGuid().ToString();
        doc.PartitionKey = DocumentType.Photo.ToString("D");
        var document = _mapper.Map<PhotoDto>(await _documentRepository.CreateEntityAsync(doc));
        return document;
    }

    public async Task<DocumentDto> CreateDocumentAsync(DocumentForCreatedDto model)
    {
        var doc = _mapper.Map<Document>(model);

        doc.FileName = await _blobRepository.UploadAsync(new MemoryStream(model.Value), model.FileName);
        doc.RowKey = Guid.NewGuid().ToString();
        doc.PartitionKey = DocumentType.Document.ToString("D");
        var document = _mapper.Map<DocumentDto>(await _documentRepository.CreateEntityAsync(doc));
        return document;
    }
}