namespace TextAnalyzer.Renderer.Interfaces;

public interface IChartRenderer
{
    Task<IChart> GenerateChart<TKey, TValue>(string chartTitle, IDictionary<TKey, TValue> data);
}