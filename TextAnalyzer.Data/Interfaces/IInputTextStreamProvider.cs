using TextAnalyzer.Infrastructure.Interfaces;

namespace TextAnalyzer.Data.Interfaces
{
    public interface IInputTextStreamProvider
    {
        Task<IEnumerable<IText>> GetInputTextsAsync();
    }
}
