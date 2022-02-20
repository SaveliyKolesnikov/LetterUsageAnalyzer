using TextAnalyzer.Infrastructure.Interfaces;
using TextAnalyzer.Services.Models;

namespace TextAnalyzer.Services.Interfaces;

public interface ITextAnalyzer
{
    Task<TextAnalysisResult> AnalyzeTextAsync(IText text);
}