using System;
using CsvProcessor.Core.Models;

namespace CsvProcessor.Core.Interfaces;

/// <summary>
/// Defines methods for reading CSV data from streams and returning
/// rows of values for further processing.
/// </summary>
public interface ICsvReader
{
    /// <summary>
    /// Reads a CSV file asynchronously from the provided stream and returns its rows.
    /// </summary>
    /// <param name="stream">
    /// The <see cref="Stream"/> containing the CSV data to be read.
    /// </param>
    /// <param name="HasHeader">
    /// A boolean value indicating whether the CSV file includes a header row.
    /// If true, the first row will be treated as column headers.
    /// </param>
    /// <param name="Delimeter">
    /// The delimiter character used to separate fields in the CSV file (e.g., "," or ";").
    /// </param>
    /// <returns>
    /// An <see cref="IAsyncEnumerable{T}"/> sequence of string arrays, 
    /// where each array represents a row of values from the CSV file.
    /// </returns>
    IAsyncEnumerable<string[]> ReadFileAsync(Stream stream, bool HasHeader, string Delimeter);
}
