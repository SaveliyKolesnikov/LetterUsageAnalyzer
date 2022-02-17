using TextAnalyzer.Infrastructure.Interfaces;
using TextAnalyzer.Infrastructure.Models;
using TextAnalyzer.Services.Interfaces;

namespace TextAnalyzer.Services.Services
{
    public class TextAnalyzerService : ITextAnalyzer
    {
        private readonly ISymbolAnalyzer symbolAnalyzer;

        public TextAnalyzerService(ISymbolAnalyzer symbolAnalyzer)
        {
            this.symbolAnalyzer = symbolAnalyzer;
        }

        public async Task<TextAnalysisResult> AnalyzeTextAsync(IText text)
        {
            var textEnumberable = text.ReadAsync();
            var analysisResult = await symbolAnalyzer.AnalyzeAsync(textEnumberable);
            return new TextAnalysisResult(text, analysisResult);
        }
    }
}
