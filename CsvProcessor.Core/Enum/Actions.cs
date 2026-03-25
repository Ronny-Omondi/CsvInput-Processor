namespace CsvProcessor.Core.Enum;

/// <summary>
/// Defines the possible actions that can be applied to a CSV record
/// or value during the filtering process.
/// </summary>
public enum Actions
{
    /// <summary>
    /// Keep the record or value in the result set.
    /// </summary>
    Keep,

    /// <summary>
    /// Remove the record or value from the result set.
    /// </summary>
    Remove,

    /// <summary>
    /// Mark the record or value for special handling 
    /// (e.g., flagging or tagging for review).
    /// </summary>
    Mark,
}