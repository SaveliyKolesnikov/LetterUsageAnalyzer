// See https://aka.ms/new-console-template for more information
using Dasync.Collections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;
using TextAnalyzer.Data.DependencyInjection;
using TextAnalyzer.Data.Interfaces;
using TextAnalyzer.Infrastructure.Interfaces;
using TextAnalyzer.Infrastructure.Models;
using TextAnalyzer.Services.DependencyInjection;

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

var grouped = textAnalysisResults.GroupBy(c => c.Text.Group);

var result = new StringBuilder();
foreach (var group in grouped)
{
    result.AppendLine($"Group name: {group.Key}");
    foreach (var textAnalysisResult in group)
    {
        result.AppendLine($"Text title: {textAnalysisResult.Text.Title}");
        foreach (var (ch, count) in textAnalysisResult.SymbolAnalysisResult.LetterUsage.OrderByDescending(c => c.Value))
        {
            result.AppendLine($"{ch}: {count}");
        }
        result.AppendLine();
    }
    var delimiter = new string('-', 80);
    result.AppendLine(delimiter);
}
var notepadPath = Path.Combine(Environment.SystemDirectory, "notepad.exe");
var filePath = Path.Combine(Environment.CurrentDirectory, "result.txt");
await File.WriteAllTextAsync(filePath, result.ToString());
var startInfo = new ProcessStartInfo(notepadPath)
{
    WindowStyle = ProcessWindowStyle.Maximized,
    Arguments = filePath
};

Process.Start(startInfo);
/*
foreach (var group in grouped)
{
    Console.WriteLine($"Group name: {group.Key}");
    foreach (var textAnalysisResult in group)
    {
        Console.WriteLine($"Text title: {textAnalysisResult.Text.Title}");
        foreach (var (ch, count) in textAnalysisResult.SymbolAnalysisResult.LetterUsage.OrderByDescending(c => c.Value))
        {
            Console.WriteLine($"{ch}: {count}");
        }
    }
    var delimiter = new string('-', 80);
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