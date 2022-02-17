using TextAnalyzer.Infrastructure.Models;

namespace TextAnalyzer.Infrastructure.Interfaces
{
    public interface ITextAnalyzer
    {
        Task<TextAnalysisResult> AnalyzeTextAsync(IText text);
    }
}
