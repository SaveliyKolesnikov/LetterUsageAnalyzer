using TextAnalyzer.Infrastructure.Interfaces;

namespace TextAnalyzer.DataProvider.Interfaces;

public interface IFileTextFactory
{
    Task<IText> GetTextAsync(string path);
}