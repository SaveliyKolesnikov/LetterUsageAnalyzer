using System.Collections.Concurrent;
using TextAnalyzer.Services.Interfaces;
using TextAnalyzer.Services.Models;

namespace TextAnalyzer.Services.Services;

public class SymbolCountAnalyzer : ISymbolAnalyzer
{
    private readonly ISymbolFilteringStratagy symbolFilteringStrategy;

    public SymbolCountAnalyzer(ISymbolFilteringStratagy symbolFilteringStrategy)
    {
        this.symbolFilteringStrategy = symbolFilteringStrategy;
    }

    public async Task<SymbolAnalysisResult> AnalyzeAsync(IAsyncEnumerable<string> text)
    {
        ConcurrentDictionary<char, decimal> symbols = new();
        await Parallel.ForEachAsync(text, (s, i) =>
        {
            var charArray = s.ToCharArray();
            Parallel.ForEach(charArray, (c, i) =>
            {
                var upperCaseSymbol = char.ToUpper(c);
                if (symbolFilteringStrategy.FilterSymbol(upperCaseSymbol))
                {
                    symbols.AddOrUpdate(upperCaseSymbol, 1, (c, oldValue) => oldValue + 1);
                }
            });
            return ValueTask.CompletedTask;
        });
        return new SymbolAnalysisResult(symbols);
    }
}