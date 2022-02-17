using TextAnalyzer.Infrastructure.Interfaces;

namespace TextAnalyzer.Services.Interfaces
{
    public interface IContentProvider
    {
        public IAsyncEnumerable<string> GetContent(IText text);
    }
}
