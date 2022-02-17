namespace TextAnalyzer.Infrastructure.Interfaces
{
    public interface ISymbolAnalysisResult
    {
        public IDictionary<char, decimal> LetterUsage { get; init; }
    }
}
