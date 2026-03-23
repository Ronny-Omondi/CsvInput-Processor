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
        var values = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        string[]? header = null;
        int[]? indexes = null;
        var input = new InputData { FilePaths = filterData.FilterCsv };
        var files = _filePath.NormalizeFilePath(input);

        await foreach (
            var row in _csvReader.ReadFileAsync(
                File.OpenRead(files[1]),
                filterData.HasHeader,
                filterData.Delimeter
            )
        )
        {
            if (header == null && filterData.HasHeader)
            {
                header = row;
                indexes = filterData
                    .Columns.Select(c => Array.IndexOf(header, c))
                    .Where(i => i >= 0)
                    .ToArray();
                continue;
            }
            foreach (var i in indexes ?? Array.Empty<int>())
            {
                if (i < row.Length && !string.IsNullOrWhiteSpace(row[i]))
                    values.Add(row[i].Trim());
            }
        }
        return values;
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
