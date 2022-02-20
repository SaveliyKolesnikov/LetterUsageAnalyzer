using TextAnalyzer.Renderer.Interfaces;

namespace TextAnalyzer.Renderer.Model;

public class Chart : IChart
{
    private readonly byte[] chartImageBytes;

    private Chart(byte[] chartImageBytes)
    {
        this.chartImageBytes = chartImageBytes;
    }

    public Task ToFileAsync(string filePath) => File.WriteAllBytesAsync(filePath, chartImageBytes);
    
    public static IChart FromBase64(string base64) => new Chart(Convert.FromBase64String(base64));
}