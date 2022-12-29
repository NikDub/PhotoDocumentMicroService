using PhotoDocumentMicroService.Domain.Entities.Models;

namespace PhotoDocumentMicroService.Infrastructure.Repository.Abstractions;

public interface IDocumentRepository
{
    Task<Document> GetEntityAsync(string type, string id);
    List<Document> GetEntityByPatientId(string patientId);
    Task<Document> CreateEntityAsync(Document entity);
}