using System;
using CsvProcessor.Core.Models;

namespace CsvProcessor.Core.Interfaces;

/// <summary>
/// Defines methods for processing CSV input data, applying filters,
/// and producing match results based on configuration.
/// </summary>
public interface ICsvProcessor
{
    /// <summary>
    /// Executes the CSV input filter process using the provided configuration.
    /// </summary>
    /// <param name="csvMatchFilter">
    /// The <see cref="CsvMatchFilterConfig"/> object that defines the filter rules,
    /// column selections, and matching criteria to be applied to the CSV input.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. 
    /// The task result contains a <see cref="CsvMatchFilterResult"/> object
    /// with the outcome of the filtering process, including matched values
    /// and any relevant metadata.
    /// </returns>
    public Task<CsvMatchFilterResult> CsvInputResult(CsvMatchFilterConfig csvMatchFilter);
}
