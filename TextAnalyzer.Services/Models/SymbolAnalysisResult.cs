using TextAnalyzer.Infrastructure.Interfaces;

namespace TextAnalyzer.Services.Models
{
    internal class SymbolAnalysisResult : ISymbolAnalysisResult
    {
        public IDictionary<char, decimal> LetterUsage { get; init; }

        public SymbolAnalysisResult(IDictionary<char, decimal> letterUsage)
        {
            LetterUsage = letterUsage;
        }
    }
}
