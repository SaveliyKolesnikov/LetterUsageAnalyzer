using Jering.Javascript.NodeJS;
using TextAnalyzer.Renderer.Interfaces;
using TextAnalyzer.Renderer.Model;

namespace TextAnalyzer.Renderer.Services;

public class ExcelChartRenderer : IChartRenderer
{
    private readonly INodeJSService nodeJsService;

    public ExcelChartRenderer(INodeJSService nodeJsService)
    {
        this.nodeJsService = nodeJsService;
    }

    public async Task<IChart> GenerateChart<TKey, TValue>(string chartTitle, IDictionary<TKey, TValue> data)
    {
        // Invoke javascript
        var res = await nodeJsService.InvokeFromFileAsync<string>("Services/chartjs-node/index.js",
                      "generateChart",
                      new object[] {chartTitle, data}) ??
                  throw new ArgumentNullException();

        var base64FromJs = res[(res.IndexOf(",", StringComparison.Ordinal) + 1)..];
        return Chart.FromBase64(base64FromJs);
    }
}