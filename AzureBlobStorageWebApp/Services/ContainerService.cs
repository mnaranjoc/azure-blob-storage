
using Azure.Storage.Blobs;

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

        public async Task<List<string>> GetAllContainer()
        {
            var containerName = new List<string>();

            await foreach (var item in _blobClient.GetBlobContainersAsync())
            {
                containerName.Add(item.Name);
            }

            return containerName;
        }

        public Task<List<string>> GetAllContainerAndBlobs()
        {
            throw new NotImplementedException();
        }
    }
}
