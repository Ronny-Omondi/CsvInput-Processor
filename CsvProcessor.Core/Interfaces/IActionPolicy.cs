using System;
using CsvProcessor.Core.Enum;
using CsvProcessor.Core.Models;

namespace CsvProcessor.Core.Interfaces;

public interface IActionPolicy
{
    /// <summary>
    /// Applies the configured action policy to a similarity score.
    /// </summary>
    /// <param name="similarity">The similarity score (0.0–1.0).</param>
    /// <param name="policy">The action policy configuration.</param>
    /// <returns>The decided action (Keep, Remove, Mark).</returns>
    Actions ApplyAction(double similarity, ActionPolicy actionPolicy);
}
