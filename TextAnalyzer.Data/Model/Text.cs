using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAnalyzer.Infrastructure.Interfaces;

namespace TextAnalyzer.Data.Model
{
    public class Text : IText
    {
        public string Title { get; set; }
        public string Format { get; init; }

        protected readonly string path;

        protected Text(string path) 
        { 
            this.path = path;
            Title = Path.GetFileName(path);
            Format = Path.GetExtension(path);
        }

        public static Text FromPath(string path) => new(path);

        public Task<Stream> ReadAsync()
        {
            Stream result = File.OpenRead(path);
            return Task.FromResult(result);
        }
    }
}
