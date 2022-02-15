// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TextAnalyzer.Data.DependencyInjection;
using TextAnalyzer.Data.Interfaces;

var serviceCollection = new ServiceCollection()
    .AddLogging(b => b.AddConsole())
    .AddDataProvider();

var serviceProvider = serviceCollection.BuildServiceProvider();

var textProvider = serviceProvider.GetRequiredService<ITextProvider>();
var texts = await textProvider.GetInputTextsAsync();



//Console.WriteLine("Hello, World!");
//EpubBook epubBook = EpubReader.ReadBook("pelevin_iphuck-10_499568_fb2.epub");

//EpubContent bookContent = epubBook.Content;
//foreach (EpubTextContentFile htmlFile in bookContent.Html.Values)
//{
//    string htmlContent = htmlFile.Content;
//}