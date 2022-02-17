using TextAnalyzer.Data.Model;
using TextAnalyzer.DataProvider.Interfaces;
using TextAnalyzer.Infrastructure.Interfaces;

namespace TextAnalyzer.DataProvider.Services
{
    public class FileTextFactory : IFileTextFactory
    {
        private const string epub = ".epub";

        public async Task<IText> GetTextAsync(string path)
        {
            var extension = Path.GetExtension(path).ToLower();
            return extension switch
            {
                epub => await EpubText.FromPath(path),
                _ => throw new ArgumentException("Unsupported file format", extension)
            };
        }
    }
}
