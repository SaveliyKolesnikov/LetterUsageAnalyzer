using Microsoft.Extensions.DependencyInjection;
using TextAnalyzer.Analyzers.Interfaces;

namespace TextAnalyzer.Analyzers.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSymbolAnalyzer(this IServiceCollection services)
        {
            // TODO: Add analyzer after implemented
            //services.AddSingleton<ISymbolAnalyzer, LocalTextProvider>();

            return services;
        }
    }
}
