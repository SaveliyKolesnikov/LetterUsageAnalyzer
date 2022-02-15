namespace TextAnalyzer.Infrastructure.Interfaces
{
    public interface ISymbolAnalysisResult
    {
        public string TextTitle { get; set; }
        public IDictionary<char, decimal> LetterUsage { get; set; }
    }
}
