using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Xml;
using System.Xml.Linq;
using TextAnalyzer.Infrastructure.Interfaces;
using TextAnalyzer.Services.Interfaces;
using VersOne.Epub;

namespace TextAnalyzer.Services.Services
{
    public class EpubContentProvider : IContentProvider
    {
        private readonly ILogger<EpubContentProvider> logger;

        public EpubContentProvider(ILogger<EpubContentProvider> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async IAsyncEnumerable<string> GetContent(IText text)
        {
            var book = await EpubReader.ReadBookAsync(await text.ReadAsync());
            var readingOrder = book.ReadingOrder;
            text.Title = book.Title;

            ConcurrentBag<string> resultText = new();
            try
            {
                Parallel.ForEach(readingOrder, (r, c) =>
                {
                    var content = r.Content;
                    var reader = XmlReader.Create(new StringReader(content), new XmlReaderSettings() { XmlResolver = null, DtdProcessing = DtdProcessing.Ignore });
                    var doc = XDocument.Load(reader);
                    var nodeValues = doc.DescendantNodes().OfType<XText>().Select(t => t.Value).Where(t => !string.IsNullOrWhiteSpace(t) && t != "\n").ToArray();
                    Parallel.ForEach(nodeValues, resultText.Add);
                });
            }
            catch (Exception e)
            {
                logger.LogError("Unexpected error during parsing epub doc", e);
            }

            foreach (var textValue in resultText)
            {
                yield return textValue;
            }
            /*
            foreach (var readingOrder in await book.GetReadingOrderAsync())
            {
                var content = await readingOrder.ReadContentAsTextAsync();
                var doc = XDocument.Load(content);
                var nodeValues = doc.DescendantNodes().OfType<XText>().Select(t => t.Value);
                foreach (var textValue in nodeValues)
                {
                    yield return textValue;
                }
            }
            */
        }
    }
}
