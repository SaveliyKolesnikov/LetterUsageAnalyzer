using Microsoft.Extensions.DependencyInjection;
using TextAnalyzer.Services.Interfaces;
using TextAnalyzer.Services.Services;
using TextAnalyzer.Services.Services.SymbolFilters;

namespace TextAnalyzer.Services.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddSymbolPercentageAnalyzer(this IServiceCollection services)
    {
        services.AddSingleton<ISymbolAnalyzer, PercentageSymbolAnalyzer>();

        services.AddFilteringStrategy();
        services.AddSingleton<ITextAnalyzer, TextAnalyzerService>();


        return services;
    }
    public static IServiceCollection AddSymbolCountAnalyzer(this IServiceCollection services)
    {
        services.AddSingleton<ISymbolAnalyzer, SymbolCountAnalyzer>();

        services.AddFilteringStrategy();
        services.AddSingleton<ITextAnalyzer, TextAnalyzerService>();


        return services;
    }

    private static IServiceCollection AddFilteringStrategy(this IServiceCollection services)
    {
        //services.Scan(c => c.FromAssemblyOf<ISymbolFilteringStratagy>().AddClasses(c => c.AssignableTo<ISymbolFilteringStratagy>()).AsImplementedInterfaces().WithTransientLifetime());

        services.AddSingleton<ISymbolFilteringStratagy, ISymbolFilteringStratagy.Default>();
        services.Decorate<ISymbolFilteringStratagy, NoNewLinesFiltering>();
        services.Decorate<ISymbolFilteringStratagy, NoWhitespacesFiltering>();
        services.Decorate<ISymbolFilteringStratagy, RussianAlphabetFiltering>();
        return services;
    }
}