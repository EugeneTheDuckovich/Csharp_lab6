using AutoMapper;
using Lab6.Infrastructure;
using Lab6.Server.Entities;
using Lab6.Server.Repositories.Abstract;
using Lab6.Server.Services.Abstract;

namespace Lab6.Server.Services;

public class FileInfoService : IFileInfoService
{
    private readonly IFileInfoRepository _fileInfoRepository;
    private readonly IMapper _mapper;

    public FileInfoService(IFileInfoRepository fileInfoRepository, IMapper mapper)
    {
        _fileInfoRepository = fileInfoRepository;
        _mapper = mapper;
    }

    public async Task AddInfoAsync(FileInfoDto fileInfo)
    {
        await _fileInfoRepository.AddInfoAsync(_mapper.Map<FileInfoEntity>(fileInfo));
    }

    public async Task DeleteinfoAsync(FileInfoDto fileInfo)
    {
        await _fileInfoRepository.DeleteInfoAsync(_mapper.Map<FileInfoEntity>(fileInfo));
    }

    public async Task<FileInfoDto?> GetInfoAsync(string id)
    {
        FileInfoEntity? entity = await _fileInfoRepository.GetInfoAsync(id);
        return _mapper.Map<FileInfoDto?>(entity);
    }

    public async Task<FileInfoDto[]> GetInfosAsync()
    {
        FileInfoEntity[] entities = await _fileInfoRepository.GetInfosAsync();
        return entities.Select(i => _mapper.Map<FileInfoDto>(i)).ToArray();
    }
}
