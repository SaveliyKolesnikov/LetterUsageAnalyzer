// See https://aka.ms/new-console-template for more information
using Dasync.Collections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TextAnalyzer.Analyzers.DependencyInjection;
using TextAnalyzer.Analyzers.Interfaces;
using TextAnalyzer.Data.DependencyInjection;
using TextAnalyzer.Data.Interfaces;

var serviceCollection = new ServiceCollection()
    .AddLogging(b => b.AddConsole())
    .AddDataProvider()
    .AddSymbolAnalyzer();

var serviceProvider = serviceCollection.BuildServiceProvider();

var textProvider = serviceProvider.GetRequiredService<ITextProvider>();
var texts = await textProvider.GetInputTextsAsync();

var symbolAnalyzer = serviceProvider.GetRequiredService<ISymbolAnalyzer>();
var analysisResults = await Task.WhenAll(texts.Select(symbolAnalyzer.AnalyzeAsync));


foreach (var result in analysisResults)
{
    Console.WriteLine(result.TextTitle);
    foreach (var (ch, count) in result.LetterUsage)
    {
        Console.WriteLine($"{ch}: {count}");
    }
    var delimiter = new string('-', 30);
    Console.WriteLine(delimiter);
}


//Console.WriteLine("Hello, World!");
//EpubBook epubBook = EpubReader.ReadBook("pelevin_iphuck-10_499568_fb2.epub");

//EpubContent bookContent = epubBook.Content;
//foreach (EpubTextContentFile htmlFile in bookContent.Html.Values)
//{
//    string htmlContent = htmlFile.Content;
//}