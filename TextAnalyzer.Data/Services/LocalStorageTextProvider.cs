using TextAnalyzer.Data.Interfaces;
using TextAnalyzer.DataProvider.Interfaces;
using TextAnalyzer.Infrastructure.Interfaces;

namespace TextAnalyzer.Data.Services
{
    public class LocalStorageTextProvider : IInputTextStreamProvider
    {
        private readonly IFileTextFactory fileTextFactory;

        public LocalStorageTextProvider(IFileTextFactory fileTextFactory)
        {
            this.fileTextFactory = fileTextFactory;
        }

        public async ValueTask<IEnumerable<IText>> GetInputTextsAsync()
        {
            var dataPath = Path.Combine(Environment.CurrentDirectory, "Data");
            var data = GetFoldersWithData(dataPath);
            return await Task.WhenAll(data.Values.SelectMany(f => f).Select(fileTextFactory.GetTextAsync));
        }

        protected static IDictionary<string, IEnumerable<string>> GetFoldersWithData(string dataPath)
        {
            var result = new Dictionary<string, IEnumerable<string>>();
            foreach (var directoryName in Directory.GetDirectories(dataPath))
            {
                result.Add(directoryName, Directory.GetFiles(directoryName));
            }
            return result;
        }
    }
}
