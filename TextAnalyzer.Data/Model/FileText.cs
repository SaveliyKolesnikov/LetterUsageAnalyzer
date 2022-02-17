using TextAnalyzer.Infrastructure.Interfaces;

namespace TextAnalyzer.Data.Model
{
    public class FileText : IText
    {
        public string Title { get; set; }
        public string Format { get; init; }
        public string Group { get; init; }

        protected readonly string path;

        protected FileText(string path)
        {
            this.path = path;
            Title = Path.GetFileName(path);
            Format = Path.GetExtension(path);
            Group = new FileInfo(path).Directory!.Name;
        }

        protected virtual Stream ReadFile()
        {
            return File.OpenRead(path);
        }

        public virtual async IAsyncEnumerable<string> ReadAsync()
        {
            using var stream = ReadFile();
            using var reader = new StreamReader(stream);
            var result = await reader.ReadToEndAsync();
            yield return result;
        }
    }
}
