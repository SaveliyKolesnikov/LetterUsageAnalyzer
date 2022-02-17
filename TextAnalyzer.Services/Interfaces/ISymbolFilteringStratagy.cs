namespace TextAnalyzer.Services.Interfaces
{
    public interface ISymbolFilteringStratagy
    {
        bool FilterSymbol(char ch);

        public class Default : ISymbolFilteringStratagy
        {
            public bool FilterSymbol(char ch) => true;
        }
    }
}
