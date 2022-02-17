using TextAnalyzer.Infrastructure.Interfaces;
using TextAnalyzer.Services.Models;

namespace TextAnalyzer.Infrastructure.Models
{
    public record TextAnalysisResult(IText Text, SymbolAnalysisResult SymbolAnalysisResult);
}