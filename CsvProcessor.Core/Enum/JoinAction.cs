namespace CsvProcessor.Core.Enum;

/// <summary>
/// Specifies how multiple sets of values should be combined
/// when building a filter.
/// </summary>
public enum JoinAction
{
    /// <summary>
    /// Combines all values from the sets into a single collection,
    /// including duplicates only once (set union).
    /// </summary>
    Union,

    /// <summary>
    /// Retains only the values that are present in all sets
    /// (set intersection).
    /// </summary>
    Intersection,
}
