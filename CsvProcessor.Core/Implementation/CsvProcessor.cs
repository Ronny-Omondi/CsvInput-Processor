using CsvProcessor.Core.Interfaces;
using CsvProcessor.Core.Models;

namespace CsvProcessor.Core.Implementation;

public class CsvProcessor : ICsvProcessor
{
    private readonly IConfigurationLoader _configuration;
    private readonly ICsvReader _csvReader;
    private readonly IFilePath _filePath;
    private readonly IFilter _filter;
    private readonly IActionPolicy _policy;
    private readonly IMatcher _matcher;
    private readonly IOutputWriter _output;

    public CsvProcessor(
        IConfigurationLoader configurationLoader,
        ICsvReader csvReader,
        IFilePath filePath,
        IFilter filter,
        IActionPolicy actionPolicy,
        IMatcher matcher,
        IOutputWriter outputWriter
    )
    {
        _configuration = configurationLoader;
        _csvReader = csvReader;
        _filePath = filePath;
        _filter = filter;
        _policy = actionPolicy;
        _matcher = matcher;
        _output = outputWriter;
    }

    public async Task<CsvMatchFilterResult> CsvInputResult(CsvMatchFilterConfig config)
    {
        var filters = await _filter.BuildFilterAsync(config.Filter);
        var results = new List<(string[] row, Enum.Actions action)>();

        var files = _filePath.NormalizeFilePath(config.Input);

        
        string[]? headers = null;
        if (config.Input.HasHeader && files.Any())
        {
            using var headerStream = File.OpenRead(files.First());
            await foreach (var headerRow in _csvReader.ReadFileAsync(headerStream, true, config.Input.Delimeter))
            {
                headers = headerRow;
                break;
            }
        }

        foreach (var path in files)
        {
            using var stream = File.OpenRead(path);

            await foreach (var row in _csvReader.ReadFileAsync(stream, config.Input.HasHeader, config.Input.Delimeter))
            {
                var columnRef = config.MatchFilter.InputColumns.First();
                int index = ResolveIndex(headers, columnRef);

                string value = row[index];
                bool matched = await _matcher.IsMatch(value, filters, config.MatchFilter);

                double similarity = matched ? 1.0 : 0.0;
                var action = _policy.ApplyAction(similarity, config.ActionPolicy);

                if (config.ActionPolicy.MarkColumn && action == Enum.Actions.Mark)
                {
                    var extendedRow = row.Append("MARKED").ToArray();
                    results.Add((extendedRow, action));
                }
                else
                {
                    results.Add((row, action));
                }
            }
        }

        await _output.WriteResultAsync(config.OutPut, results);

    
        return null;
    }

    private int ResolveIndex(string[]? headers, string columnRef)
    {
        // If numeric string, treat as index
        if (int.TryParse(columnRef, out var index))
            return index;

        if (headers == null)
            throw new InvalidOperationException("Cannot resolve column name without headers.");

        int resolved = Array.IndexOf(headers, columnRef);
        if (resolved == -1)
            throw new ArgumentException($"Column '{columnRef}' not found in headers.");
        return resolved;
    }
}