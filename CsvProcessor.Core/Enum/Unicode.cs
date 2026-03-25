namespace CsvProcessor.Core.Enum;

/// <summary>
/// Specifies the Unicode normalization form to apply when processing text.
/// Normalization ensures that equivalent sequences of characters are represented
/// in a consistent way for comparison or storage.
/// </summary>
public enum Unicode
{
    /// <summary>
    /// Normalization Form C (NFC): 
    /// canonical composition, where characters are combined into their composed form.
    /// For example, "é" is stored as a single composed character rather than "e" + accent.
    /// </summary>
    NFC,

    /// <summary>
    /// Normalization Form KC (NFKC): 
    /// compatibility composition, which applies canonical composition and also
    /// replaces characters with their compatibility equivalents.
    /// For example, superscript numbers or ligatures are converted to their standard forms.
    /// </summary>
    NFKC,
}
