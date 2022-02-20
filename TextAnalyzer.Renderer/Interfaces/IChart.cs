namespace TextAnalyzer.Renderer.Interfaces;

public interface IChart
{
    Task ToFileAsync(string filePath);
}