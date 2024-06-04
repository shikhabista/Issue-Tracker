using Microsoft.AspNetCore.Http;

namespace Base.Helpers.Interfaces;

public interface IFileHelper
{
    Task<string> UploadFile(IFormFile file, string subDirectory = "");
    Task RemoveFile(string fileName, string subDirectory = "");
}