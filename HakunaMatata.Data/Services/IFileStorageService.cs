using HakunaMatata.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HakunaMatata.Data.Services
{
    public interface IFileStorageService
    {
        Task<UrlsDto> UploadAsync(ICollection<FileDto> files);
    }
}
