using TextAnalyzer.DataProvider.Interfaces;
using TextAnalyzer.DataProvider.Model;
using TextAnalyzer.Infrastructure.Interfaces;

namespace TextAnalyzer.DataProvider.Services;

public class FileTextFactory : IFileTextFactory
{
    private const string Epub = ".epub";

    public async Task<IText> GetTextAsync(string path)
    {
        var extension = Path.GetExtension(path).ToLower();
        return extension switch
        {
            Epub => await EpubText.FromPath(path),
            _ => throw new ArgumentException("Unsupported file format", extension)
        };
    }
}