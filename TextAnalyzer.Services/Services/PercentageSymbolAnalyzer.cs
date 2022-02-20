using System.Collections.Concurrent;
using TextAnalyzer.Services.Interfaces;
using TextAnalyzer.Services.Models;

namespace TextAnalyzer.Services.Services;

public class PercentageSymbolAnalyzer : ISymbolAnalyzer
{
  private readonly ISymbolFilteringStratagy symbolFilteringStrategy;

  public PercentageSymbolAnalyzer(ISymbolFilteringStratagy symbolFilteringStrategy)
  {
    this.symbolFilteringStrategy = symbolFilteringStrategy;
  }

  public async Task<SymbolAnalysisResult> AnalyzeAsync(IAsyncEnumerable<string> text)
  {
    ConcurrentDictionary<char, decimal> symbols = new();
    var charactersCount = 0u;
    await Parallel.ForEachAsync(text, (s, i) =>
    {
      var charArray = s.ToCharArray();
      var charNumber = (uint)charArray.Length;
      Interlocked.Add(ref charactersCount, charNumber);
      Parallel.ForEach(charArray, (c, i) =>
      {
        var upperCaseSymbol = char.ToUpper(c);
        if (symbolFilteringStrategy.FilterSymbol(upperCaseSymbol))
        {
          symbols.AddOrUpdate(upperCaseSymbol, 1, (_, oldValue) => oldValue + 1);
        }
      });

      return ValueTask.CompletedTask;
    });

    var percentageResult = symbols.ToDictionary(c => c.Key, v => (v.Value / charactersCount) * 100);

    return new SymbolAnalysisResult(percentageResult);
  }
}