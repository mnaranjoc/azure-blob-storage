namespace AzureBlobStorageWebApp.Models
{
    public class ContainerModel
    {
        public required string Name { get; set; }
        public required List<string> Blobs { get; set; }
    }
}
