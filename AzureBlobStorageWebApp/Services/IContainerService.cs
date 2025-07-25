﻿using AzureBlobStorageWebApp.Models;

namespace AzureBlobStorageWebApp.Services
{
    public interface IContainerService
    {
        // Containers
        Task<List<ContainerModel>> GetAllContainerAndBlobs();
        Task<List<ContainerModel>> GetAllContainer();
        Task CreateContainer(string containerName);
        Task DeleteContainer(string containerName);

        // Blobs
        Task<bool> DeleteBlob(string name, string containerName);
        Task<string> PreviewBlob(string name, string containerName);
        Task<bool> CreateBlob(string name, string containerName, IFormFile file);
        Task UpdateBlobMetadata(string name, string containerName);
        Task DeleteBlobMetadata(string name, string containerName);
        Task<Uri> GeneratePublicURL(string name, string containerName);
    }
}
