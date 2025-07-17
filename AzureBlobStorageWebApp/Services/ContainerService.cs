
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureBlobStorageWebApp.Models;

namespace AzureBlobStorageWebApp.Services
{
    public class ContainerService : IContainerService
    {
        private readonly BlobServiceClient _blobClient;

        public ContainerService(BlobServiceClient blobClient)
        {
            _blobClient = blobClient;
        }

        public async Task<bool> CreateBlob(string name, string containerName, IFormFile file)
        {
            var blobContainerClient = _blobClient.GetBlobContainerClient(containerName);
            var blobClient = blobContainerClient.GetBlobClient(name);
            var httpHeaders = new BlobHttpHeaders()
            {
                ContentType = file.ContentType
            };
            var metadata = new Dictionary<string, string>() { { "Metadata1", "value" }, { "Metadata2", "value" } };

            var result = await blobClient.UploadAsync(file.OpenReadStream(), httpHeaders, metadata);

            return result != null;
        }

        public async Task CreateContainer(string containerName)
        {
            await _blobClient.CreateBlobContainerAsync(containerName);
        }

        public async Task<bool> DeleteBlob(string name, string containerName)
        {
            var blobContainerClient = _blobClient.GetBlobContainerClient(containerName);
            var blobClient = blobContainerClient.GetBlobClient(name);
            return await blobClient.DeleteIfExistsAsync();
        }

        public async Task DeleteContainer(string containerName)
        {
            await _blobClient.DeleteBlobContainerAsync(containerName);
        }

        public async Task<List<ContainerModel>> GetAllContainer()
        {
            var containers = new List<ContainerModel>();

            await foreach (var item in _blobClient.GetBlobContainersAsync())
            {
                containers.Add(new() { Name = item.Name, Blobs = [] });
            }

            return containers;
        }

        public async Task<List<ContainerModel>> GetAllContainerAndBlobs()
        {
            var containers = await GetAllContainer();
            foreach (var container in containers)
            {
                var blobContainerClient = _blobClient.GetBlobContainerClient(container.Name);
                var blobs = blobContainerClient.GetBlobsAsync();
                await foreach (var blob in blobs)
                {
                    var blobClient = blobContainerClient.GetBlobClient(blob.Name);
                    var properties = await blobClient.GetPropertiesAsync();
                    container.Blobs.Add(new BlobModel() { Name = blob.Name, Metadata = properties.Value.Metadata });
                }
            }

            return containers;
        }

        public async Task<string> PreviewBlob(string name, string containerName)
        {
            var blobContainerClient = _blobClient.GetBlobContainerClient(containerName);
            var blobClient = blobContainerClient.GetBlobClient(name);
            return blobClient.Uri.AbsoluteUri;
        }

        public async Task UpdateBlobMetadata(string name, string containerName)
        {
            var blobContainerClient = _blobClient.GetBlobContainerClient(containerName);
            var blobClient = blobContainerClient.GetBlobClient(name);

            await blobClient.SetMetadataAsync(
                new Dictionary<string, string>()
                {
                    { "Metadata1", Guid.NewGuid().ToString() },
                    { "Metadata2", Guid.NewGuid().ToString() }
                }
            );
        }
    }
}
