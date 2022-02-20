using TextAnalyzer.Services.Models;

namespace TextAnalyzer.Services.Interfaces;

public interface ISymbolAnalyzer
{
    Task<SymbolAnalysisResult> AnalyzeAsync(IAsyncEnumerable<string> text);
}