using Lab6.Server.Entities;
using Lab6.Server.Persistence;
using Lab6.Server.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Server.Repositories;

public class FileInfoRepository : IFileInfoRepository
{
    private FileDataContext _context;

    public FileInfoRepository(FileDataContext context)
    {
        _context = context;
    }

    public async Task AddInfoAsync(FileInfoEntity fileInfo)
    {
        await _context.FileInfos.AddAsync(fileInfo);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteInfoAsync(FileInfoEntity fileInfo)
    {
         _context.FileInfos.Remove(fileInfo);
        await _context.SaveChangesAsync();
    }

    public async Task<FileInfoEntity?> GetInfoAsync(string id)
    {
        return await _context.FileInfos.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<FileInfoEntity[]> GetInfosAsync()
    {
        return _context.FileInfos.ToArray();
    }
}