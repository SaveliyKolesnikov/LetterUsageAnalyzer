using TextAnalyzer.Services.Interfaces;

namespace TextAnalyzer.Services.Services.SymbolFilters;

public class NoWhitespacesFiltering : ISymbolFilteringStratagy
{
    private readonly ISymbolFilteringStratagy inner;

    public NoWhitespacesFiltering(ISymbolFilteringStratagy inner)
    {
        this.inner = inner;
    }

    public bool FilterSymbol(char ch) => inner.FilterSymbol(ch) && ch != ' ';
}