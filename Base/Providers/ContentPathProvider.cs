using Base.Extensions;
using Base.Providers.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Base.Providers;

public class ContentPathProvider : IContentPathProvider
{
    private readonly IConfiguration _configuration;

    public ContentPathProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> GetPath(DirectoryType type)
    {
        var leafDirectory = type switch
        {
            DirectoryType.Attachments => "attachments",
            DirectoryType.Backup => "backups",
            DirectoryType.DbRestore => "restored_dbs",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
        var contentRootPath = _configuration["ContentDir"];
        if (contentRootPath.IsNullOrEmpty())
            throw new Exception("Error: contentDir is not set, Please contact software vendor");
        var path = Path.Combine(contentRootPath!, leafDirectory!);
        EnsureDirectoryIsCreated(path);
        return path;
    }

    private void EnsureDirectoryIsCreated(string ensureDir)
    {
        if (!Directory.Exists(ensureDir)) Directory.CreateDirectory(ensureDir!);
    }
}

public enum DirectoryType
{
    Attachments = 1,
    Backup = 2,
    DbRestore = 3
}