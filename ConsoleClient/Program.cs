// See https://aka.ms/new-console-template for more information
using Dasync.Collections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Diagnostics;
using TextAnalyzer.Data.DependencyInjection;
using TextAnalyzer.Data.Interfaces;
using TextAnalyzer.Infrastructure.Interfaces;
using TextAnalyzer.Services.DependencyInjection;
using TextAnalyzer.Services.Interfaces;
using VersOne.Epub;

var serviceCollection = new ServiceCollection()
    .AddLogging(b => b.AddConsole())
    .AddDataProvider()
    .AddSymbolAnalyzer();

var serviceProvider = serviceCollection.BuildServiceProvider();

var textProvider = serviceProvider.GetRequiredService<IInputTextStreamProvider>();
var texts = await textProvider.GetInputTextsAsync();

var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

var contentProvider = serviceProvider.GetRequiredService<IContentProvider>();
var symbolAnalyzer = serviceProvider.GetRequiredService<ISymbolAnalyzer>();
var contentProviders = texts.Select(contentProvider.GetContent).ToArray();

ConcurrentBag<ISymbolAnalysisResult> analysisResults = new();
await Parallel.ForEachAsync(contentProviders, async (contentProvider, c) =>
{
    var analysisResult = await symbolAnalyzer.AnalyzeAsync(contentProvider);
    analysisResults.Add(analysisResult);
});


foreach (var result in analysisResults)
{
    //Console.WriteLine(result.TextTitle);
    foreach (var (ch, count) in result.LetterUsage)
    {
        Console.WriteLine($"{ch}: {count}");
    }
    var delimiter = new string('-', 30);
    Console.WriteLine(delimiter);
}

/*

//Console.WriteLine("Hello, World!");
//EpubBook epubBook = EpubReader.ReadBook("pelevin_iphuck-10_499568_fb2.epub");

//EpubContent bookContent = epubBook.Content;
//foreach (EpubTextContentFile htmlFile in bookContent.Html.Values)
//{
//    string htmlContent = htmlFile.Content;
//}
*/