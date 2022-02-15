﻿using TextAnalyzer.Infrastructure.Interfaces;

namespace TextAnalyzer.Data.Interfaces
{
    public interface ITextProvider
    {
        Task<IEnumerable<IText>> GetInputTextsAsync();
    }
}