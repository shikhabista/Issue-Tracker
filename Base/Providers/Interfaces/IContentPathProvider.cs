namespace Base.Providers.Interfaces;

public interface IContentPathProvider
{
    Task<string> GetPath(DirectoryType type);
}