using Microsoft.Extensions.DependencyInjection;
using TextAnalyzer.Data.Interfaces;
using TextAnalyzer.Data.Services;
using TextAnalyzer.DataProvider.Interfaces;
using TextAnalyzer.DataProvider.Services;

namespace TextAnalyzer.Data.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataProvider(this IServiceCollection services)
        {
            services.AddSingleton<IInputTextStreamProvider, LocalStorageTextProvider>();
            services.AddSingleton<IFileTextFactory, FileTextFactory>();

            return services;
        }
    }
}
