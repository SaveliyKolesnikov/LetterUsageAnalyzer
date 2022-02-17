using Microsoft.Extensions.DependencyInjection;
using TextAnalyzer.Services.Interfaces;
using TextAnalyzer.Services.Services;

namespace TextAnalyzer.Services.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSymbolAnalyzer(this IServiceCollection services)
        {
            // TODO: Add analyzer after implemented
            services.AddSingleton<ISymbolAnalyzer, SymbolAnalyzer>();
            services.AddSingleton<IContentProvider, EpubContentProvider>();

            return services;
        }
    }
}
