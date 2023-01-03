using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using PhotoDocumentMicroService.Domain.Entities.Models;
using PhotoDocumentMicroService.Infrastructure.Repository.Abstractions;

namespace PhotoDocumentMicroService.Infrastructure.Repository;

public class DocumentRepository : IDocumentRepository
{
    private readonly TableClient _tableClient;

    public DocumentRepository(TableServiceClient tableServiceClient, IConfiguration configuration)
    {
        _tableClient = tableServiceClient.GetTableClient(configuration["Azure:Table"]);
        _tableClient.CreateIfNotExists();
    }

    public async Task<Document> GetEntityAsync(string type, string id)
    {
        return await _tableClient.GetEntityAsync<Document>(type, id);
    }

    public List<Document> GetEntityByResultId(string resultId)
    {
        var documents = _tableClient.Query<Document>(r => r.ResultId == resultId);
        return documents.ToList();
    }

    public async Task<Document> CreateEntityAsync(Document entity)
    {
        await _tableClient.UpsertEntityAsync(entity);
        return entity;
    }
}