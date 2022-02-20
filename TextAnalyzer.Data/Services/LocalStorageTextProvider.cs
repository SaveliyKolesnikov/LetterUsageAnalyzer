using TextAnalyzer.DataProvider.Interfaces;
using TextAnalyzer.Infrastructure.Interfaces;

namespace TextAnalyzer.DataProvider.Services;

public class LocalStorageTextProvider : IInputTextStreamProvider
{
    private readonly IFileTextFactory fileTextFactory;

    public LocalStorageTextProvider(IFileTextFactory fileTextFactory)
    {
        this.fileTextFactory = fileTextFactory;
    }

    public async ValueTask<IEnumerable<IText>> GetInputTextsAsync()
    {
        var dataPath = Path.Combine(Environment.CurrentDirectory, "Data");
        var data = GetFoldersWithData(dataPath);
        return await Task.WhenAll(data.Values.SelectMany(f => f).Select(fileTextFactory.GetTextAsync));
    }

    protected static IDictionary<string, IEnumerable<string>> GetFoldersWithData(string dataPath)
    {
        return Directory.GetDirectories(dataPath)
            .ToDictionary<string?, string, IEnumerable<string>>(directoryName => directoryName, Directory.GetFiles);
    }
}