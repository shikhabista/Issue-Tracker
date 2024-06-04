using Base.Helpers.Interfaces;
using Base.Providers;
using Base.Providers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Base.Helpers;

public class FileHelper : IFileHelper
{
    private readonly IContentPathProvider _userUploadFilePathProvider;

    public FileHelper(IContentPathProvider userUploadFilePathProvider)
    {
        _userUploadFilePathProvider = userUploadFilePathProvider;
    }

    public async Task<string> UploadFile(IFormFile file, string subDirectory = "")
    {
        var rootPath = await GetRootPath();
        var path = Path.Combine(rootPath, subDirectory);
        EnsureDirectoryIsCreated(path);
        var extension = Path.GetExtension(file.FileName);
        var encryptedFileName = Guid.NewGuid() + extension;
        var filePath = Path.Combine(path, encryptedFileName);
        await using var stream = new FileStream(filePath, FileMode.Create); 
        await file.CopyToAsync(stream);
        return encryptedFileName;
    }

    private async Task<string> GetRootPath() => await _userUploadFilePathProvider.GetPath(DirectoryType.Attachments);

    public async Task RemoveFile(string fileName, string subDirectory)
    {
        var rootPath = await GetRootPath();
        var directory = Path.Combine(rootPath, !subDirectory.IsNullOrEmpty() ? subDirectory : string.Empty);
        var path = Path.Combine(Directory.GetCurrentDirectory(), directory, fileName);
        if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(path)) return;
        File.Delete(path);
    }
        
    private static void EnsureDirectoryIsCreated(string rootDirectory)
    {
        if (!Directory.Exists(rootDirectory)) Directory.CreateDirectory(rootDirectory);
    }
}