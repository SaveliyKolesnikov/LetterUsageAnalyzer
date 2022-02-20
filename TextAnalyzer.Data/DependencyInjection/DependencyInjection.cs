using Microsoft.Extensions.DependencyInjection;
using TextAnalyzer.DataProvider.Interfaces;
using TextAnalyzer.DataProvider.Services;

namespace TextAnalyzer.DataProvider.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddDataProvider(this IServiceCollection services)
    {
        services.AddSingleton<IInputTextStreamProvider, LocalStorageTextProvider>();
        services.AddSingleton<IFileTextFactory, FileTextFactory>();

        return services;
    }
}