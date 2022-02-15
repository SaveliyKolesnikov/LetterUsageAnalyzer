namespace TextAnalyzer.Infrastructure.Interfaces
{
    public interface IText
    {
        public string Title { get; set; }
        public Task<Stream> ReadAsync();
    }
}
