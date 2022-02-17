using TextAnalyzer.Data.Interfaces;
using TextAnalyzer.Data.Model;
using TextAnalyzer.Infrastructure.Interfaces;

namespace TextAnalyzer.Data.Services
{
    public class LocalStorageTextProvider : IInputTextStreamProvider
    {
        public Task<IEnumerable<IText>> GetInputTextsAsync()
        {
            var dataPath = Path.Combine(Environment.CurrentDirectory, "Data");
            var data = GetFoldersWithData(dataPath);
            IEnumerable<IText> texts = GenerateTextWrapperFromFilePaths(data).ToArray();

            return Task.FromResult(texts);
        }

        protected virtual IEnumerable<IText> GenerateTextWrapperFromFilePaths(IDictionary<string, IEnumerable<string>> data)
        {
            return data.Values.SelectMany(f => f).Select(Text.FromPath);
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
