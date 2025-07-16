
using Azure.Storage.Blobs;
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

        public async Task CreateContainer(string containerName)
        {
            await _blobClient.CreateBlobContainerAsync(containerName);
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
                    container.Blobs.Add(blob.Name);
                }
            }

            return containers;
        }
    }
}
