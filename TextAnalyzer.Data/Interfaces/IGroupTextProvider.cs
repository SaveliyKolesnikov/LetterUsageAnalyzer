using TextAnalyzer.Infrastructure.Interfaces;

namespace TextAnalyzer.Data.Interfaces
{
    public interface IGroupTextProvider
    {
        Task<IEnumerable<IGroupText>> GetInputTextsAsync();
    }
}
