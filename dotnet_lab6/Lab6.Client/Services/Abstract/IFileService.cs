using Lab6.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab6.Client.Services.Abstract;

public interface IFileService
{
    Task<IEnumerable<FileInfoDto>> GetInfosAsync();
    Task DownloadAsync(FileInfoDto fileInfo, string savePath);
    Task UploadAsync(string path);
}
