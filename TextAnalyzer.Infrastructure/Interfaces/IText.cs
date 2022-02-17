namespace TextAnalyzer.Infrastructure.Interfaces
{
    public interface IText : ITextSource
    {
        public string Title { get; set; }
        public string Group { get; init; }
    }
}
