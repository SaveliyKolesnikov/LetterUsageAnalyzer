using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalyzer.Infrastructure.Interfaces
{
    public interface ITextSource
    {
        public Task<Stream> ReadAsync();
    }
}
