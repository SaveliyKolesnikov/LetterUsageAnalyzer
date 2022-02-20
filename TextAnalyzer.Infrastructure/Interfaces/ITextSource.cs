namespace TextAnalyzer.Infrastructure.Interfaces;

public interface ITextSource
{
    public IAsyncEnumerable<string> ReadAsync();
}