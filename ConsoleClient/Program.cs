using System.Collections.Concurrent;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TextAnalyzer.DataProvider.DependencyInjection;
using TextAnalyzer.DataProvider.Interfaces;
using TextAnalyzer.Infrastructure.Interfaces;
using TextAnalyzer.Renderer.DependencyInjection;
using TextAnalyzer.Renderer.Interfaces;
using TextAnalyzer.Services.DependencyInjection;
using TextAnalyzer.Services.Interfaces;
using TextAnalyzer.Services.Models;

var serviceCollection = new ServiceCollection()
    .AddLogging(b => b.AddConsole())
    .AddDataProvider()
    .AddAnalyzers()
    .AddChartRenderer();


var serviceProvider = serviceCollection.BuildServiceProvider();

var textProvider = serviceProvider.GetRequiredService<IInputTextStreamProvider>();

var logger = serviceProvider.GetRequiredService<ILogger<Program>>();


var texts = await textProvider.GetInputTextsAsync();
var textAnalyzer = serviceProvider.GetRequiredService<ITextAnalyzer>();

var analysisTimeTracker = Stopwatch.StartNew();
ConcurrentBag<TextAnalysisResult> textAnalysisResults = new();
await Parallel.ForEachAsync(texts, async (text, c) =>
{
    var analysisResult = await textAnalyzer.AnalyzeTextAsync(text);
    textAnalysisResults.Add(analysisResult);
});
analysisTimeTracker.Stop();

var renderingTimeTracker = Stopwatch.StartNew();
const string chartsDirectory = "Charts";
await Task.WhenAll(
    WriteChartsToDirectory(chartsDirectory, textAnalysisResults, serviceProvider),
    WriteGroupedChartsToDirectory(chartsDirectory, textAnalysisResults, serviceProvider)
);
renderingTimeTracker.Stop();

logger.LogInformation("Time spent for analysis: {ms} ms", analysisTimeTracker.ElapsedMilliseconds);
logger.LogInformation("Time spent for rendering: {ms} ms", renderingTimeTracker.ElapsedMilliseconds);

Console.ReadLine();

async Task WriteChartsToDirectory(string outputDirectory, IEnumerable<TextAnalysisResult> analysisResults,
    IServiceProvider serviceProvider)
{
    var chartRenderer = serviceProvider.GetRequiredService<IChartRenderer>();
    Directory.CreateDirectory(outputDirectory);
    foreach (var groupedResults in analysisResults.GroupBy(c => c.Text.Group))
        await Parallel.ForEachAsync(groupedResults, async (textAnalysisResult, c) =>
        {
            var (text, symbolAnalysisResult) = textAnalysisResult;

            // Stub for rus alphabet letters that are not usually used for unified charts.
            if (!symbolAnalysisResult.LetterUsage.ContainsKey('Ё'))
                symbolAnalysisResult.LetterUsage.Add('Ё', decimal.Zero);
            if (!symbolAnalysisResult.LetterUsage.ContainsKey('Ъ'))
                symbolAnalysisResult.LetterUsage.Add('Ъ', decimal.Zero);

            // Custom order rule for rus Ё letter due to its code that does not match the alphabet order.
            var orderedByChar = symbolAnalysisResult.LetterUsage
                .OrderBy(c => c.Key == 'Ё' ? 'Е' + 1 : c.Key)
                .ToDictionary(c => c.Key, c => c.Value);

            var chart = await chartRenderer.GenerateChart(text.Title, orderedByChar);


            var outputDirectoryPath = Path.Combine(outputDirectory, text.Group);
            Directory.CreateDirectory(outputDirectoryPath);
            var filePath = Path.Combine(outputDirectoryPath, $"{text.Title}.png");
            await chart.ToFileAsync(filePath);
        });
}

async Task WriteGroupedChartsToDirectory(string outputDirectory, IEnumerable<TextAnalysisResult> analysisResults,
    IServiceProvider serviceProvider)
{
    var chartRenderer = serviceProvider.GetRequiredService<IChartRenderer>();
    Directory.CreateDirectory(outputDirectory);
    foreach (var groupedResults in analysisResults.GroupBy(c => c.Text.Group))
    {
        var resultDictionary = new Dictionary<char, decimal>();
        foreach (var textAnalysisResult in groupedResults)
        {
            foreach (var (ch, count) in textAnalysisResult.SymbolAnalysisResult.LetterUsage)
            {
                if (resultDictionary.ContainsKey(ch))
                {
                    resultDictionary[ch] += count;
                }
                else
                {
                    resultDictionary[ch] = count;
                }
            }
        }

        var orderedDictionary = resultDictionary.OrderBy(c => c.Key == 'Ё' ? 'Е' + 1 : c.Key)
            .ToDictionary(c => c.Key, c => c.Value);

        var chart = await chartRenderer.GenerateChart(groupedResults.Key, orderedDictionary);


        var filePath = Path.Combine(outputDirectory, $"{groupedResults.Key}.png");
        await chart.ToFileAsync(filePath);
    }
}

//var result = new StringBuilder();
//foreach (var group in grouped)
//{
//result.AppendLine($"Group name: {group.Key}");
//var resultDictionary = new Dictionary<char, decimal>();
//foreach (var textAnalysisResult in group)
//{
//    foreach (var (ch, count) in textAnalysisResult.SymbolAnalysisResult.LetterUsage)
//    {
//        if (resultDictionary.ContainsKey(ch))
//        {
//            resultDictionary[ch] += count;
//        }
//        else
//        {
//            resultDictionary[ch] = count;
//        }
//    }
//}

//    foreach (var (ch, count) in resultDictionary.OrderByDescending(c => c.Value))
//    {
//        result.AppendLine($"{ch}: {count}");
//    }
//    result.AppendLine();
//    var delimiter = new string('-', 80);
//    result.AppendLine(delimiter);
//}

//var notepadPath = Path.Combine(Environment.SystemDirectory, "notepad.exe");
//var filePath = Path.Combine(Environment.CurrentDirectory, "result.txt");
//await File.WriteAllTextAsync(filePath, result.ToString());
//var startInfo = new ProcessStartInfo(notepadPath)
//{
//    WindowStyle = ProcessWindowStyle.Maximized,
//    Arguments = filePath
//};

//Process.Start(startInfo);