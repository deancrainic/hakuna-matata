using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using HakunaMatata.Data.DTOs;
using HakunaMatata.Data.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Data.Services
{

    public class FileStorageService : IFileStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _storageConnectionString;

        public FileStorageService(IConfiguration config)
        {
            _storageConnectionString = config.GetConnectionString("AzureStorage");
            _blobServiceClient = new BlobServiceClient(_storageConnectionString);
        }

        public async Task<UrlsDto> UploadAsync(ICollection<FileDto> files)
        {
            if (files == null || files.Count() == 0)
            {
                return null;
            }
            
            var containerClient = _blobServiceClient.GetBlobContainerClient("hakunamatatauploads");

            var urls = new List<string>();

            foreach (var file in files)
            {
                var blobClient = containerClient.GetBlobClient(file.GetPathWithFileName());

                await blobClient.UploadAsync(file.Content, new BlobHttpHeaders { ContentType = file.ContentType });

                urls.Add(blobClient.Uri.ToString());
            }

            return new UrlsDto(urls);
        }
    }
}
