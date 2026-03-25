using System;
using CsvProcessor.Core.Models;

namespace CsvProcessor.Core.Interfaces;

/// <summary>
/// Defines methods for applying matching logic to input values,
/// supporting exact and fuzzy comparison strategies.
/// </summary>
public interface IMatcher
{
    /// <summary>
    /// Determines whether the specified input string matches any of the provided filter values
    /// according to the given matching strategy.
    /// </summary>
    /// <param name="input">
    /// The input string to evaluate against the filter set.
    /// </param>
    /// <param name="filters">
    /// A <see cref="HashSet{String}"/> containing the filter values to compare against.
    /// </param>
    /// <param name="match">
    /// The matching strategy to apply, defined by the <see cref="Matchs"/> enumeration.
    /// This may represent exact match, partial match, fuzzy match, etc., depending on implementation.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. 
    /// The task result is <c>true</c> if the input string satisfies the matching criteria
    /// against the filter set; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> IsMatch(string input, HashSet<string> filters, Matchs match);
}
