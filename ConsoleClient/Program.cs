// See https://aka.ms/new-console-template for more information
using Dasync.Collections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using TextAnalyzer.Data.DependencyInjection;
using TextAnalyzer.Data.Interfaces;
using TextAnalyzer.Infrastructure.Interfaces;
using TextAnalyzer.Infrastructure.Models;
using TextAnalyzer.Services.DependencyInjection;
using TextAnalyzer.Services.Interfaces;

var serviceCollection = new ServiceCollection()
    .AddLogging(b => b.AddConsole())
    .AddDataProvider()
    .AddAnalyzers();


var serviceProvider = serviceCollection.BuildServiceProvider();

var textProvider = serviceProvider.GetRequiredService<IInputTextStreamProvider>();

var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

var texts = await textProvider.GetInputTextsAsync();
var textAnalyzer = serviceProvider.GetRequiredService<ITextAnalyzer>();

ConcurrentBag<TextAnalysisResult> textAnalysisResults = new();
await Parallel.ForEachAsync(texts, async (text, c) =>
{
    var analysisResult = await textAnalyzer.AnalyzeTextAsync(text);
    textAnalysisResults.Add(analysisResult);
});


foreach (var textAnalysisResult in textAnalysisResults)
{
    Console.WriteLine($"Text group: {textAnalysisResult.Text.Group}");
    Console.WriteLine($"Text title: {textAnalysisResult.Text.Title}");
    foreach (var (ch, count) in textAnalysisResult.SymbolAnalysisResult.LetterUsage)
    {
        Console.WriteLine($"{ch}: {count}");
    }
    var delimiter = new string('-', 30);
    Console.WriteLine(delimiter);
}

Console.ReadLine();
/*

//Console.WriteLine("Hello, World!");
//EpubBook epubBook = EpubReader.ReadBook("pelevin_iphuck-10_499568_fb2.epub");

//EpubContent bookContent = epubBook.Content;
//foreach (EpubTextContentFile htmlFile in bookContent.Html.Values)
//{
//    string htmlContent = htmlFile.Content;
//}
*/