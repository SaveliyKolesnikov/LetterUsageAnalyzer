using System.Collections.Concurrent;
using TextAnalyzer.Services.Interfaces;
using TextAnalyzer.Services.Models;

namespace TextAnalyzer.Services.Services
{
    public class SymbolAnalyzer : ISymbolAnalyzer
    {
        private readonly ISymbolFilteringStratagy symbolFilteringStratagy;

        public SymbolAnalyzer(ISymbolFilteringStratagy symbolFilteringStratagy)
        {
            this.symbolFilteringStratagy = symbolFilteringStratagy;
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
                    if (symbolFilteringStratagy.FilterSymbol(upperCaseSymbol))
                    {
                        symbols.AddOrUpdate(upperCaseSymbol, 1, (c, oldValue) => oldValue + 1);
                    }
                });
                return ValueTask.CompletedTask;
            });
            return new SymbolAnalysisResult(symbols);
        }
    }
}
