using Lab6.Client.Services.Abstract;
using Lab6.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab6.Client.Services;

public class FileService : IFileService
{
    private const string BaseUrl = "https://localhost:5000/api/files";
    public async Task<IEnumerable<FileInfoDto>> GetInfosAsync()
    {
        var httpClient = new HttpClient();
        HttpResponseMessage response = await httpClient.GetAsync($"{BaseUrl}/all");
        string content = await response.Content.ReadAsStringAsync();
        FileInfoDto[] result = JsonSerializer.Deserialize<FileInfoDto[]>(content);
        return result;
    }

    public async Task DownloadAsync(FileInfoDto fileInfo, string savePath)
    {
        HttpClient client = new HttpClient();

        HttpResponseMessage fileResponse = await client.GetAsync($"{BaseUrl}/download/{fileInfo.Id}");
        using (var fileStream = new FileStream($"{savePath}/{fileInfo.Name}", FileMode.Create))
        {
            await fileResponse.Content.CopyToAsync(fileStream);
        }
    }

    public async Task UploadAsync(string path)
    {
        HttpClient client = new HttpClient();
        using (var multipartFormContent = new MultipartFormDataContent())
        {
            //Load the file and set the file's Content-Type header
            var fileStreamContent = new StreamContent(File.OpenRead(path));
            fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            string fileName = path.Split('\\').Last();
            //Add the file
            multipartFormContent.Add(fileStreamContent, "file", fileName);

            //Send it
            var response = await client.PostAsync($"{BaseUrl}/upload", multipartFormContent);
            response.EnsureSuccessStatusCode();
        }
    }
}
