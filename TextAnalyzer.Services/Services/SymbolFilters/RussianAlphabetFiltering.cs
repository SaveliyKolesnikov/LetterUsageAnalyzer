using TextAnalyzer.Services.Interfaces;

namespace TextAnalyzer.Services.Services.SymbolFilters;

public class RussianAlphabetFiltering : ISymbolFilteringStratagy
{
    private static readonly HashSet<char> letters =
        "АаБбВвГгДдЕеЁёЖжЗзИиЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЪъЫыЬьЭэЮюЯя".ToCharArray().ToHashSet();

    private readonly ISymbolFilteringStratagy inner;

    public RussianAlphabetFiltering(ISymbolFilteringStratagy inner)
    {
        this.inner = inner;
    }

    public bool FilterSymbol(char ch) => inner.FilterSymbol(ch) && letters.Contains(ch);
}