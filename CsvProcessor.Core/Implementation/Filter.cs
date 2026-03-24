using System;
using CsvProcessor.Core.Enum;
using CsvProcessor.Core.Interfaces;
using CsvProcessor.Core.Models;

namespace CsvProcessor.Core.Implementation;

public class Filter : IFilter
{
    private readonly ICsvReader _csvReader;
    private readonly IFilePath _filePath;

    public Filter(ICsvReader csvReader, IFilePath filePath)
    {
        _csvReader = csvReader;
        _filePath = filePath;
    }

    public async Task<HashSet<string>> BuildFilterAsync(FilterData filterData)
    {
        if (filterData == null)
            throw new ArgumentNullException("Filter data must be provided");
        var sets = new List<HashSet<string>>();

        foreach (var file in filterData.FilterCsv) //filter csv list
        {
            var set = await ExtractValues(filterData);
            if (set.Count > 0)
                sets.Add(set);
        }
        return FilterSetBuilder(sets, filterData.JoinBy);
    }

    private async Task<HashSet<string>> ExtractValues(FilterData filterData)
    {
        throw new NotImplementedException();
    }

    private HashSet<string> FilterSetBuilder(IEnumerable<IEnumerable<string>> sets, JoinAction mode)
    {
        HashSet<string> filtered = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
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
