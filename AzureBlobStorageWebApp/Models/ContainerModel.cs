namespace AzureBlobStorageWebApp.Models
{
    public class ContainerModel
    {
        public required string Name { get; set; }
        public required List<BlobModel> Blobs { get; set; }
    }

    public class BlobModel
    {
        public required string Name { get; set; }
        public required IDictionary<string, string> Metadata { get; set; }
    }
}
