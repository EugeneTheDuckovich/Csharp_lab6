using Lab6.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Server.Persistence;

public class FileDataContext : DbContext
{
    public DbSet<FileInfoEntity> FileInfos { get; set; }

    public FileDataContext(DbContextOptions<FileDataContext> options)
        : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();

        //FileInfos.Add(new FileInfoEntity
        //{
        //    Id = Guid.NewGuid().ToString(),
        //    Name = "file1.txt",
        //});
        //FileInfos.Add(new FileInfoEntity
        //{
        //    Id = Guid.NewGuid().ToString(),
        //    Name = "file2.txt",
        //});
        //SaveChanges();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
