using Lab6.Infrastructure;
using Lab6.Server.Services.Abstract;
using System.Net;
using System.Net.Http.Headers;

namespace Lab6.Server.Services;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<FileStream?> GetFile(FileInfoDto fileInfo)
    {
        string filePath = $"{_webHostEnvironment.WebRootPath}/files/{fileInfo.Id}-{fileInfo.Name}";
        if (!File.Exists(filePath))
        {
            return null;
        }
        return new FileStream(filePath, FileMode.Open);
    }

    public async Task Save(IFormFile file, string id)
    {
        string path = $"/files/{id}-{file.FileName}";
        
        using (var fileStream = new FileStream(_webHostEnvironment.WebRootPath + path, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }
    }
}
