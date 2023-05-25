using Lab6.Infrastructure;
using Lab6.Server.Services.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Lab6.Server.Controllers;

[Route("api/files")]
[ApiController]
public class FileController : ControllerBase
{
    private IFileService _fileService;
    private IFileInfoService _fileInfoService;
    private readonly IWebHostEnvironment _webHostEnvironment;


    public FileController(IFileService fileService, IFileInfoService fileInfoService, IWebHostEnvironment webHostEnvironment)
    {
        _fileService = fileService;
        _fileInfoService = fileInfoService;
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        FileInfoDto[] fileInfos = await _fileInfoService.GetInfosAsync();
        return Ok(fileInfos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        FileInfoDto? fileInfo = await _fileInfoService.GetInfoAsync(id);
        return Ok(fileInfo);
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        var fileInfo = new FileInfoDto()
        {
            Id = Guid.NewGuid().ToString(),
            Name = file.FileName,
        };

        await _fileInfoService.AddInfoAsync(fileInfo);
        await _fileService.Save(file,fileInfo.Id);

        return Ok(fileInfo);
    }

    [HttpGet("download/{id}")]
    public async Task<IActionResult> Download(string id)
    {
        FileInfoDto fileInfo = await _fileInfoService.GetInfoAsync(id);
        FileStream? stream = await _fileService.GetFile(fileInfo);
        if(stream is null) return NotFound();
        return File(stream, "application/octet-stream",fileInfo.Name);
    }
}
