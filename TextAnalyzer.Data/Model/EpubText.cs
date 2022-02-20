using System.Collections.Concurrent;
using System.Xml;
using System.Xml.Linq;
using VersOne.Epub;

namespace TextAnalyzer.DataProvider.Model;

public class EpubText : FileText
{
    private EpubText(string path) : base(path)
    {
    }

    public override async IAsyncEnumerable<string> ReadAsync()
    {
        using var fileStream = ReadFile();
        var book = await EpubReader.ReadBookAsync(fileStream);
        Title = book.Title;
        var readingOrder = book.ReadingOrder;

        ConcurrentBag<string> resultText = new();
        Parallel.ForEach(readingOrder, (r, c) =>
        {
            var content = r.Content;
            var reader = XmlReader.Create(new StringReader(content),
                new XmlReaderSettings {XmlResolver = null, DtdProcessing = DtdProcessing.Ignore});
            var doc = XDocument.Load(reader);
            var nodeValues = doc.DescendantNodes().OfType<XText>().Select(t => t.Value)
                .Where(t => !string.IsNullOrWhiteSpace(t) && t != "\n").ToArray();
            Parallel.ForEach(nodeValues, resultText.Add);
        });

        foreach (var textValue in resultText) yield return textValue;
    }

    public static async Task<EpubText> FromPath(string path)
    {
        var epubText = new EpubText(path);
        epubText.Title = await epubText.GetEpubBookTitleAsync();
        return epubText;
    }

    private async Task<string> GetEpubBookTitleAsync()
    {
        await using var fileStream = ReadFile();
        var book = await EpubReader.ReadBookAsync(fileStream);
        return book.Title;
    }
}