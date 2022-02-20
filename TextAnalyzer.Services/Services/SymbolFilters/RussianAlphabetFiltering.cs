﻿using TextAnalyzer.Services.Interfaces;

namespace TextAnalyzer.Services.Services.SymbolFilters
{
    public class RussianAlphabetFiltering : ISymbolFilteringStratagy
    {
        private readonly ISymbolFilteringStratagy inner;

        public RussianAlphabetFiltering(ISymbolFilteringStratagy inner)
        {
            this.inner = inner;
        }

        private static readonly HashSet<char> letters = "АаБбВвГгДдЕеЁёЖжЗзИиЙйКкЛлМмНнОоПпСсТтУуФфХхЦцЧчШшЩщЪъЫыЬьЭэЮюЯя".ToCharArray().ToHashSet();

        public bool FilterSymbol(char ch) => inner.FilterSymbol(ch) && letters.Contains(ch);
    }
}