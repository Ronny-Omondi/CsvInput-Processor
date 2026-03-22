using System;
using CsvProcessor.Core.Enum;
using CsvProcessor.Core.Interfaces;

namespace CsvProcessor.Core.Implementation;

public class Filter : IFilter
{
    public async Task<HashSet<string>> FilterSetBuilder(
        IEnumerable<IEnumerable<string>> sets,
        JoinAction mode
    )
    {
        HashSet<string> filtered = null!;
        if (sets == null)
            throw new ArgumentNullException("Filter set cannot be null");
        foreach (var set in sets)
        {
            if (filtered == null)
            {
                filtered = [.. set];
            }
            else
            {
                if (mode == JoinAction.Union)
                {
                    filtered.UnionWith(set);
                }
                else
                {
                    filtered.IntersectWith(set);
                }
            }
        }

        return filtered;
    }
}
