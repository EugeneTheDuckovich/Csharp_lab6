using AutoMapper;
using Lab6.Infrastructure;
using Lab6.Server.Entities;

namespace Lab6.Server.Mappers;

public class ApplicationMapper : Profile
{
    public ApplicationMapper()
    {
        CreateMap<FileInfoEntity,FileInfoDto>().ReverseMap();
    }
}
