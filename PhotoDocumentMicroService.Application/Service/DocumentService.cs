using AutoMapper;
using PhotoDocumentMicroService.Application.DTO;
using PhotoDocumentMicroService.Application.Service.Abstractions;
using PhotoDocumentMicroService.Domain.Entities.Enums;
using PhotoDocumentMicroService.Domain.Entities.Models;
using PhotoDocumentMicroService.Infrastructure.Repository.Abstractions;

namespace PhotoDocumentMicroService.Application.Service;

public class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IBlobRepository _blobRepository;
    private readonly IMapper _mapper;

    public DocumentService(IDocumentRepository documentRepository, IBlobRepository blobRepository, IMapper mapper)
    {
        _documentRepository = documentRepository;
        _blobRepository = blobRepository;
        _mapper = mapper;
    }

    public async Task<DocumentDto> GetByIdAsync(string id) // account|office 
    {
        var doc = await _documentRepository.GetEntityAsync(DocumentTypeEnum.Document.ToString("D"), id);
        var blobDoc = _blobRepository.DownloadAsync(doc.FileName);

        var docDto = _mapper.Map<DocumentDto>(doc);
        docDto.Value = (await blobDoc).ToArray();
        return docDto;
    }

    public async Task<List<DocumentDto>> GetByPatientAsyncId(string resultId) // schedule
    {
        var doc = _documentRepository.GetEntityByPatientId(resultId);

        var docDto = _mapper.Map<List<DocumentDto>>(doc);
        foreach (var item in docDto)
        {
            item.Value = (await _blobRepository.DownloadAsync(item.FileName)).ToArray();
        }
        return docDto;
    }

    public async Task<DocumentDto> CreateAsync(DocumentForCreatedDto model, string docType) // account|office|schedule
    {
        var doc = _mapper.Map<Document>(model);

        await _blobRepository.UploadAsync(new MemoryStream(model.Value), model.FileName);
        doc.RowKey = Guid.NewGuid().ToString();
        doc.PartitionKey = docType;
        var document = _mapper.Map<DocumentDto>(await _documentRepository.CreateEntityAsync(doc));
        return document;
    }
}