using Lab6.Server.Entities;

namespace Lab6.Server.Repositories.Abstract;

public interface IFileInfoRepository
{
    Task<FileInfoEntity[]> GetInfosAsync();
    Task<FileInfoEntity?> GetInfoAsync(string id);
    Task AddInfoAsync(FileInfoEntity fileInfo);
    Task DeleteInfoAsync(FileInfoEntity fileInfo);
}
