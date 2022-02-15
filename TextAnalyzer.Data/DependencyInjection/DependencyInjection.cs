using Microsoft.Extensions.DependencyInjection;
using TextAnalyzer.Data.Interfaces;

namespace TextAnalyzer.Data.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataProvider(this IServiceCollection services)
        {
            // TODO: Add text provider after implemented
            //services.AddSingleton<ITextProvider, LocalTextProvider>();

            return services;
        }
    }
}
