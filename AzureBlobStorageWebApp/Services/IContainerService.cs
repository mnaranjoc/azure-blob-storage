using AzureBlobStorageWebApp.Models;

namespace AzureBlobStorageWebApp.Services
{
    public interface IContainerService
    {
        Task<List<ContainerModel>> GetAllContainerAndBlobs();
        Task<List<ContainerModel>> GetAllContainer();
        Task CreateContainer(string containerName);
        Task DeleteContainer(string containerName);
    }
}
