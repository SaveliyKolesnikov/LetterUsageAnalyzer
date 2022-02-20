using Jering.Javascript.NodeJS;
using Microsoft.Extensions.DependencyInjection;
using TextAnalyzer.Renderer.Interfaces;
using TextAnalyzer.Renderer.Services;

namespace TextAnalyzer.Renderer.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddChartRenderer(this IServiceCollection services)
        {
            services.AddNodeJS();
            services.AddSingleton<IChartRenderer, ExcelChartRenderer>();

            return services;
        }
    }
}
