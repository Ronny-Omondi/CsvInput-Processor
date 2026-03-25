namespace CsvProcessor.Core.Enum;

/// <summary>
/// Specifies the matching mode to use when comparing input values
/// against filter criteria.
/// </summary>
public enum Mode
{
    /// <summary>
    /// Performs an exact match, requiring the input value
    /// to be identical to the filter value.
    /// </summary>
    Exact,

    /// <summary>
    /// Performs a fuzzy match, allowing for approximate
    /// similarity between the input value and the filter value
    /// (e.g., using Levenshtein distance or other algorithms).
    /// </summary>
    Fuzzy,
}