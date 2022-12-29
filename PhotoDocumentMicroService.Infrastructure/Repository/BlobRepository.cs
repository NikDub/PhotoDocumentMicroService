using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using PhotoDocumentMicroService.Infrastructure.Repository.Abstractions;

namespace PhotoDocumentMicroService.Infrastructure.Repository;

public class BlobRepository : IBlobRepository
{
    private readonly BlobContainerClient _blobContainerClient;

    public BlobRepository(BlobServiceClient blobServiceClient)
    {
        _blobContainerClient = blobServiceClient.GetBlobContainerClient("test1");
    }

    public async Task<string> UploadAsync(Stream fileStream, string fileName)
    {
        var newFileName = DateTime.UtcNow.ToString("O") + "_" + fileName;
        var createResponse = await _blobContainerClient.CreateIfNotExistsAsync();
        if (createResponse != null && createResponse.GetRawResponse().Status == 201)
            await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.Blob);
        var blob = _blobContainerClient.GetBlobClient(newFileName);
        await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
        await blob.UploadAsync(fileStream);
        return blob.Uri.ToString();
    }

    public async Task<MemoryStream> DownloadAsync(string fileName)
    {
        await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.Blob);
        var blob = _blobContainerClient.GetBlobClient(fileName);
        var fileStream = new MemoryStream();
        await blob.DownloadToAsync(fileStream);
        fileStream.Position = 0;
        return fileStream;
    }
}