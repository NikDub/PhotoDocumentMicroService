using PhotoDocumentMicroService.Domain.Entities.Models;

namespace PhotoDocumentMicroService.Infrastructure.Repository.Abstractions;

public interface IDocumentRepository
{
    Task<Document> GetEntityAsync(string type, string id);
    List<Document> GetEntityByResultId(string resultId);
    Task<Document> CreateEntityAsync(Document entity);
}