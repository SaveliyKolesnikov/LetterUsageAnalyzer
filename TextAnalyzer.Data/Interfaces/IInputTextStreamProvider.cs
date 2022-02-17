using TextAnalyzer.Infrastructure.Interfaces;

namespace TextAnalyzer.Data.Interfaces
{
    public interface IInputTextStreamProvider
    {
        ValueTask<IEnumerable<IText>> GetInputTextsAsync();
    }
}
