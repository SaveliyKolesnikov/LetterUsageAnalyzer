using TextAnalyzer.Data.Interfaces;
using TextAnalyzer.Data.Model;
using TextAnalyzer.Infrastructure.Interfaces;

namespace TextAnalyzer.Data.Services
{
    internal class LocalStorageTextProvider : ITextProvider
    {
        public Task<IEnumerable<IText>> GetInputTextsAsync()
        {
            var dataPath = Path.Combine(Environment.CurrentDirectory, "Data");
            var data = GetFoldersWithData(dataPath);
            IEnumerable<IText> texts = GenerateTextWrapperFromFilePaths(data).ToArray();
            return Task.FromResult(texts);
        }

        protected virtual IEnumerable<IText> GenerateTextWrapperFromFilePaths(Dictionary<string, IEnumerable<string>> data)
        {
            foreach (var file in data.Values.SelectMany(f => f))
            {
                yield return Text.FromPath(file);
            }
        }

        protected static Dictionary<string, IEnumerable<string>> GetFoldersWithData(string dataPath)
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
