namespace PhotoDocumentMicroService.Infrastructure.Repository.Abstractions;

public interface IBlobRepository
{
    Task<string> UploadAsync(Stream fileStream, string fileName);
    Task<MemoryStream> DownloadAsync(string fileName);
    string GetUri(string fileName);
}