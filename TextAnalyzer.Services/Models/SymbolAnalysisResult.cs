namespace TextAnalyzer.Services.Models;

public record SymbolAnalysisResult(IDictionary<char, decimal> LetterUsage);