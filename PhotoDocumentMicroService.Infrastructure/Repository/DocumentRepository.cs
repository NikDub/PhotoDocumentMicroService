using Azure.Data.Tables;
using PhotoDocumentMicroService.Domain.Entities.Models;
using PhotoDocumentMicroService.Infrastructure.Repository.Abstractions;

namespace PhotoDocumentMicroService.Infrastructure.Repository;

public class DocumentRepository : IDocumentRepository
{
    private readonly TableClient _tableClient;

    public DocumentRepository(TableServiceClient tableServiceClient)
    {
        _tableClient = tableServiceClient.GetTableClient("test2");
        _tableClient.CreateIfNotExists();
    }

    public async Task<Document> GetEntityAsync(string type, string id)
    {
        return await _tableClient.GetEntityAsync<Document>(type, id);
    }

    public List<Document> GetEntityByPatientId(string patientId)
    {
        var documents = _tableClient.Query<Document>(r => r.PatientId == patientId);
        return documents.ToList();
    }

    public async Task<Document> CreateEntityAsync(Document entity)
    {
        await _tableClient.UpsertEntityAsync(entity);
        return entity;
    }
}