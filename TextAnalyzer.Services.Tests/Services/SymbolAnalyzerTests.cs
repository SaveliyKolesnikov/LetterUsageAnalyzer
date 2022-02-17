using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAnalyzer.Services.Services;

namespace TextAnalyzer.Services.Tests
{
    [TestClass]
    public class SymbolAnalyzerTests
    {
        private readonly SymbolAnalyzer _analyzer = new();

        [TestMethod]
        public async Task AnalyzeAsync_ShouldAnalyzeProperlyAsync()
        {
            Dictionary<char, decimal> expectedResult = new()
            {
                { 'þ', 100000},
                { 'á', 100000},
                { 'è', 100000},
                { 'é', 100000},
                { 'å', 100000},
                { 'ó', 100000},
                { 'ñ', 100000},
            };
            async IAsyncEnumerable<string> getStrings()
            {
                var rand = new Random();
                var randomSorted = expectedResult.Keys.OrderBy(c => rand.Next(-1, 1) > 0).ToArray();
                var i = 0;
                foreach (var (ch, count) in expectedResult)
                {
                    yield return GenerateStringWithSymbols((int)count / 2, ch.ToString(), randomSorted[i++].ToString());
                }
                yield return GenerateStringWithSymbols(10000, " ");
            }

            var res = await _analyzer.AnalyzeAsync(getStrings());

            Assert.AreEqual(expectedResult.Count, res.LetterUsage.Count);
            foreach (var (ch, count) in expectedResult)
            {
                var value = res.LetterUsage[ch];
                Assert.AreEqual(count, value, $"{ch} count is not valid.");
            }
        }


        private static string GenerateStringWithSymbols(int times, params string[] symbols)
        {
            var sb = new StringBuilder();
            foreach (var symbol in symbols)
            {
                sb.Insert(0, symbol, times);
            }
            return sb.ToString();
        }
    }
}