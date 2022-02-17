using Microsoft.Extensions.Logging;
using System.Diagnostics;
using TextAnalyzer.Infrastructure.Interfaces;
using TextAnalyzer.Infrastructure.Models;
using TextAnalyzer.Services.Interfaces;

namespace TextAnalyzer.Services.Services
{
    public class TextAnalyzerService : ITextAnalyzer
    {
        private readonly ISymbolAnalyzer symbolAnalyzer;
        private readonly ILogger<TextAnalyzerService> logger;

        public TextAnalyzerService(ISymbolAnalyzer symbolAnalyzer, ILogger<TextAnalyzerService> logger)
        {
            this.symbolAnalyzer = symbolAnalyzer;
            this.logger = logger;
        }

        public async Task<TextAnalysisResult> AnalyzeTextAsync(IText text)
        {
            var time = Stopwatch.StartNew();
            var textEnumberable = text.ReadAsync();
            var analysisResult = await symbolAnalyzer.AnalyzeAsync(textEnumberable);
            logger.LogInformation("Analysis of {title} has taken: {ms} ms", text.Title, time.ElapsedMilliseconds);
            return new TextAnalysisResult(text, analysisResult);
        }
    }
}
