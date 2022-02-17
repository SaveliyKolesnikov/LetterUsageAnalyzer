using TextAnalyzer.Services.Interfaces;

namespace TextAnalyzer.Services.Services.SymbolFilters
{
    public class NoNewLinesFiltering : ISymbolFilteringStratagy
    {
        private readonly ISymbolFilteringStratagy inner;

        public NoNewLinesFiltering(ISymbolFilteringStratagy inner)
        {
            this.inner = inner;
        }

        public bool FilterSymbol(char ch) => inner.FilterSymbol(ch) && ch != '\n';
    }
}
