using Azure;
using Azure.Data.Tables;

namespace PhotoDocumentMicroService.Domain.Entities.Models;

public class Document : ITableEntity
{
    public Guid ResultId { get; set; }
    public string FileName { get; set; }

    public string RowKey { get; set; }
    public string PartitionKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}