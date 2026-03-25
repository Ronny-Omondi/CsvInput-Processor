namespace CsvProcessor.Core.Enum;

/// <summary>
/// Specifies the strategy used to break text into tokens
/// for matching or analysis.
/// </summary>
public enum TokenizationMode
{
    /// <summary>
    /// Splits text into tokens based on whitespace characters
    /// (e.g., spaces, tabs, newlines).
    /// </summary>
    Whitespace,

    /// <summary>
    /// Splits text into tokens using punctuation marks
    /// (e.g., commas, periods, semicolons) as delimiters.
    /// </summary>
    Punctuation,

    /// <summary>
    /// Splits text into overlapping sequences of characters
    /// of a fixed length (n‑grams), useful for fuzzy matching
    /// and similarity analysis.
    /// </summary>
    Ngrams,
}
