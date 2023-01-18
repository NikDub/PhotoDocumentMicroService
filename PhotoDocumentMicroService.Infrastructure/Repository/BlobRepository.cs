using System.Net;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using PhotoDocumentMicroService.Infrastructure.Repository.Abstractions;

namespace PhotoDocumentMicroService.Infrastructure.Repository;

public class BlobRepository : IBlobRepository
{
    private readonly BlobContainerClient _blobContainerClient;

    public BlobRepository(BlobServiceClient blobServiceClient, IConfiguration configuration)
    {
        _blobContainerClient = blobServiceClient.GetBlobContainerClient(configuration["Azure:Blob"]);
        var createResponse = _blobContainerClient.CreateIfNotExists();
        if (createResponse != null && createResponse.GetRawResponse().Status == (int)HttpStatusCode.Created)
            _blobContainerClient.SetAccessPolicy(PublicAccessType.Blob);
    }

    public async Task<string> UploadAsync(Stream fileStream, string fileName)
    {
        var newFileName = DateTime.UtcNow.ToString("O") + "_" + fileName;
        var blob = _blobContainerClient.GetBlobClient(newFileName);
        await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
        await blob.UploadAsync(fileStream);
        return newFileName;
    }

    public async Task<MemoryStream> DownloadAsync(string fileName)
    {
        var blob = _blobContainerClient.GetBlobClient(fileName);
        var fileStream = new MemoryStream();
        await blob.DownloadToAsync(fileStream);
        fileStream.Position = 0;
        return fileStream;
    }
}