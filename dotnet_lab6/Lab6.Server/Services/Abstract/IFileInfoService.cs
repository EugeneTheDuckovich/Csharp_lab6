using Lab6.Infrastructure;
using Lab6.Server.Entities;

namespace Lab6.Server.Services.Abstract;

public interface IFileInfoService
{
    Task<FileInfoDto[]> GetInfosAsync();
    Task<FileInfoDto?> GetInfoAsync(string id);
    Task AddInfoAsync(FileInfoDto fileInfo);
    Task DeleteinfoAsync(FileInfoDto fileInfo);
}
