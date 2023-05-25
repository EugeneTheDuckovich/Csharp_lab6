using Lab6.Client.MVVM.Infrastructure;
using Lab6.Client.Services;
using Lab6.Client.Services.Abstract;
using Lab6.Infrastructure;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lab6.Client.ViewModels;

public class MainViewModel : NotifyPropertyChanged
{
    private IFileService _fileService;
    private ObservableCollection<FileInfoDto> _fileInfos;
    private FileInfoDto _selectedInfo;
    private string _uploadPath;
    private string _savePath;    

    public MainViewModel()
    {
        _fileService = new FileService();
        IEnumerable<FileInfoDto> fileInfos = new List<FileInfoDto>();
        Task.WaitAll(Task.Run(async () =>
        {
            fileInfos = await _fileService.GetInfosAsync();
        }));
        FileInfos = new ObservableCollection<FileInfoDto>(fileInfos);
        UploadPath = string.Empty;
        SavePath = Directory.GetCurrentDirectory();
    }

    public MainViewModel(IFileService fileService)
    {
        _fileService = fileService;
        IEnumerable<FileInfoDto> fileInfos = new List<FileInfoDto>();
        Task.WaitAll(Task.Run(async () =>
        {
            fileInfos = await _fileService.GetInfosAsync();
        }));
        _fileInfos = new ObservableCollection<FileInfoDto>(fileInfos);
        _uploadPath = string.Empty;
        _savePath = Directory.GetCurrentDirectory();
    }

    public string UploadPath
    {
        get { return _uploadPath; }
        set 
        { 
            _uploadPath = value;
            OnPropertyChanged(nameof(UploadPath));
        }
    }

    public string SavePath
    {
        get { return _savePath; }
        set
        {
            _savePath = value;
            OnPropertyChanged(nameof(SavePath));
        }
    }

    public ObservableCollection<FileInfoDto> FileInfos
    {
        get { return _fileInfos; }
        set
        {
            _fileInfos = value;
            OnPropertyChanged(nameof(FileInfos));
        }
    }

    public FileInfoDto SelectedInfo
    {
        get { return  _selectedInfo; }
        set
        {
            _selectedInfo = value;
            OnPropertyChanged(nameof(SelectedInfo));
        }
    }

    public ICommand UpdateInfosCommand => new RelayCommand(parameter =>
    {
        UpdateInfosList();
    });

    public ICommand UploadFileCommand => new RelayCommand(parameter =>
    {
        Task.WaitAll(Task.Run(async () =>
        {
            await _fileService.UploadAsync(UploadPath);
        }));
        UpdateInfosList();
    });

    public ICommand DownloadFileCommand => new RelayCommand(parameter =>
    {
        Task.WaitAll(Task.Run(async () =>
        {
            await _fileService.DownloadAsync(_selectedInfo, SavePath);
        }));
    });

    public ICommand BrowseCommand => new RelayCommand(parameter =>
    {
        var fileDialog = new OpenFileDialog();

        if (fileDialog.ShowDialog() is true)
        {
            UploadPath = fileDialog.FileName;
        }
    });

    private void UpdateInfosList()
    {
        FileInfos.Clear();
        IEnumerable<FileInfoDto> fileInfos = new List<FileInfoDto>();
        Task.WaitAll(Task.Run(async () =>
        {
            fileInfos = await _fileService.GetInfosAsync();
        }));
        foreach (FileInfoDto fileInfo in fileInfos)
        {
            FileInfos.Add(fileInfo);
        }
    }
}
