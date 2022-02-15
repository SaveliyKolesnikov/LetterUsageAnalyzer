using TextAnalyzer.Infrastructure.Interfaces;

namespace TextAnalyzer.Analyzers.Interfaces
{
    public interface ISymbolAnalyzer
    {
        Task<ISymbolAnalysisResult> AnalyzeAsync(IText text);
    }
}
