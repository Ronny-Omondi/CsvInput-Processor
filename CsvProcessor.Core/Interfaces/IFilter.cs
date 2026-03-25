using System;
using CsvProcessor.Core.Enum;
using CsvProcessor.Core.Models;

namespace CsvProcessor.Core.Interfaces;

/// <summary>
/// Defines methods for building and applying filters to CSV data,
/// including set operations and matching strategies.
/// </summary>
public interface IFilter
{
    /// <summary>
    /// Builds a filter set by extracting values from the provided filter data
    /// and applying the specified join action (union or intersection).
    /// </summary>
    /// <param name="filterData">
    /// The <see cref="FilterData"/> object containing file path, delimiter,
    /// column selections, and join mode configuration.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. 
    /// The task result contains a <see cref="HashSet{String}"/> of filtered values,
    /// combined according to the join action specified in <paramref name="filterData"/>.
    /// </returns>
    public Task<HashSet<string>> BuildFilterAsync(FilterData filterData);
}
