using Microsoft.Extensions.DependencyInjection;
using TextAnalyzer.Data.Interfaces;
using TextAnalyzer.Data.Services;

namespace TextAnalyzer.Data.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataProvider(this IServiceCollection services)
        {
            services.AddSingleton<ITextProvider, LocalStorageTextProvider>();

            return services;
        }
    }
}
