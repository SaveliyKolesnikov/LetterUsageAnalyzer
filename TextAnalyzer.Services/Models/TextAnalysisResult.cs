using TextAnalyzer.Infrastructure.Interfaces;

namespace TextAnalyzer.Services.Models;

public record TextAnalysisResult(IText Text, SymbolAnalysisResult SymbolAnalysisResult);