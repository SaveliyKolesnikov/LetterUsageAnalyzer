using System.Diagnostics;
using Microsoft.Extensions.Logging;
using TextAnalyzer.Infrastructure.Interfaces;
using TextAnalyzer.Services.Interfaces;
using TextAnalyzer.Services.Models;

namespace TextAnalyzer.Services.Services;

public class TextAnalyzerService : ITextAnalyzer
{
    private readonly ILogger<TextAnalyzerService> logger;
    private readonly ISymbolAnalyzer symbolAnalyzer;

    public TextAnalyzerService(ISymbolAnalyzer symbolAnalyzer, ILogger<TextAnalyzerService> logger)
    {
        this.symbolAnalyzer = symbolAnalyzer;
        this.logger = logger;
    }

    public async Task<TextAnalysisResult> AnalyzeTextAsync(IText text)
    {
        var time = Stopwatch.StartNew();
        var textEnumerable = text.ReadAsync();
        var analysisResult = await symbolAnalyzer.AnalyzeAsync(textEnumerable);
        logger.LogInformation("Analysis of {title} has taken: {ms} ms", text.Title, time.ElapsedMilliseconds);
        return new TextAnalysisResult(text, analysisResult);
    }
}