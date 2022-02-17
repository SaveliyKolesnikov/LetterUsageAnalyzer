using TextAnalyzer.Infrastructure.Interfaces;

namespace TextAnalyzer.Services.Interfaces
{
    public interface ISymbolAnalyzer
    {
        Task<ISymbolAnalysisResult> AnalyzeAsync(IAsyncEnumerable<string> text);
    }
}
