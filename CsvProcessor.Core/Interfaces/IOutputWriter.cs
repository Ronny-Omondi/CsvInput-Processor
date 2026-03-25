using System;
using CsvProcessor.Core.Enum;
using CsvProcessor.Core.Models;

namespace CsvProcessor.Core.Interfaces;

public interface IOutputWriter
{
    /// <summary>
    /// Writes the results of CSV processing to the configured output.
    /// </summary>
    /// <param name="output">Output configuration (directory, file names).</param>
    /// <param name="results">Rows with their decided actions.</param>
    Task WriteResultAsync(OutPut output, IEnumerable<(string[] row, Actions action)> results);
}

