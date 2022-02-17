using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAnalyzer.Infrastructure.Interfaces;
using TextAnalyzer.Services.Interfaces;
using TextAnalyzer.Services.Models;

namespace TextAnalyzer.Services.Services
{
    public class SymbolAnalyzer : ISymbolAnalyzer
    {
        public async Task<ISymbolAnalysisResult> AnalyzeAsync(IAsyncEnumerable<string> text)
        {
            ConcurrentDictionary<char, decimal> symbols = new();
            await Parallel.ForEachAsync(text, (s, i) =>
            {
                var charArray = s.ToCharArray();
                Parallel.ForEach(charArray, (c, i) =>
                {
                    if (c != ' ')
                    {
                        symbols.AddOrUpdate(c, 1, (c, oldValue) => oldValue + 1);
                    }
                });
                return ValueTask.CompletedTask;
            });
            return new SymbolAnalysisResult(symbols);
        }
    }
}
