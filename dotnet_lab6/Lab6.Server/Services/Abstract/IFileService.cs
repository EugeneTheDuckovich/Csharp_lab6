using Lab6.Infrastructure;

namespace Lab6.Server.Services.Abstract;

public interface IFileService
{
    Task Save(IFormFile file, string id);
    Task<FileStream> GetFile(FileInfoDto fileInfo);
}