using TextAnalyzer.Infrastructure.Interfaces;

namespace TextAnalyzer.DataProvider.Model;

public class FileText : IText
{
    protected readonly string path;

    protected FileText(string path)
    {
        this.path = path;
        Title = Path.GetFileName(path);
        Format = Path.GetExtension(path);
        Group = new FileInfo(path).Directory!.Name;
    }

    public string Format { get; init; }
    public string Title { get; set; }
    public string Group { get; init; }

    public virtual async IAsyncEnumerable<string> ReadAsync()
    {
        await using var stream = ReadFile();
        using var reader = new StreamReader(stream);
        var result = await reader.ReadToEndAsync();
        yield return result;
    }

    protected virtual Stream ReadFile()
    {
        return File.OpenRead(path);
    }
}