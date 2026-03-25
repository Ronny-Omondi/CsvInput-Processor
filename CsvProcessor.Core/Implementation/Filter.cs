using System;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
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

        return FilterSetBuilder(await ExtractValues(filterData), filterData.JoinBy);
    }

    public async Task<IEnumerable<IEnumerable<string>>> ExtractValues(FilterData filterData)
    {
        if (filterData == null) throw new ArgumentNullException("Filter data cannot be null");
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = filterData.HasHeader,
            Delimiter = filterData.Delimeter,
        };

        using var reader = new StreamReader(filterData.FilePath);
        using var csvReader = new CsvReader(reader, config);
        if (filterData.HasHeader)
        {
            await csvReader.ReadAsync();
            csvReader.ReadHeader();
        }

        var columnList = new List<HashSet<string>>();
        for (int i = 0; i < filterData.Columns.Count; i++)
        {
            columnList.Add(new HashSet<string>());
        }
        while (await csvReader.ReadAsync())
        {
            for (int i = 0; i < filterData.Columns.Count; i++)
            {
                var col = filterData.Columns[i];
                var value = csvReader.GetField(col);
                if (!string.IsNullOrEmpty(value))
                    columnList[i].Add(value);
            }
        }
        return columnList;
    }

    public HashSet<string> FilterSetBuilder(IEnumerable<IEnumerable<string>> sets, JoinAction mode)
    {
        HashSet<string> filtered = null!;
        if (sets == null)
            throw new ArgumentNullException("Filter set cannot be null");
        foreach (var set in sets)
        {
            if (filtered == null)
            {
                filtered = new HashSet<string>(set, StringComparer.OrdinalIgnoreCase);
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

        return filtered ?? new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    }
}
