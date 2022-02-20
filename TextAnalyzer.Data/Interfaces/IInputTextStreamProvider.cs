using TextAnalyzer.Infrastructure.Interfaces;

namespace TextAnalyzer.DataProvider.Interfaces;

public interface IInputTextStreamProvider
{
    ValueTask<IEnumerable<IText>> GetInputTextsAsync();
}