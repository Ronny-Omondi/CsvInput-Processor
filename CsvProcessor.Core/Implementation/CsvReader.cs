using System;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvProcessor.Core.Interfaces;
using CsvProcessor.Core.Models;

namespace CsvProcessor.Core.Implementation;

public class CsvFileReader : ICsvReader
{
    public async IAsyncEnumerable<string[]> ReadFileAsync(
        Stream stream,
        bool HasHeader,
        string Delimeter
    )
    {
        if (stream == null)
            throw new ArgumentNullException("Stream cannot be null");
        if (string.IsNullOrWhiteSpace(Delimeter))
            Delimeter = ",";

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = HasHeader,
            Delimiter = Delimeter,
        };

        using var streamReader = new StreamReader(stream, leaveOpen: true);

        using var csvReader = new CsvReader(streamReader, config);

        if (HasHeader)
            await csvReader.ReadAsync();
        // csvReader.ReadHeader();

        while (await csvReader.ReadAsync())
        {
            var row = csvReader.Parser.Record;
            if (row != null)
                yield return row;
        }
    }
}

// public class CsvProcessor
// {
//     private readonly IConfigurationLoader _configuration;
//     private readonly ICsvReader _csvReader;
//     private readonly IFilePath _filePath;
//     private readonly IFilter _filter;
//     private readonly IActionPolicy _policy;
//     private readonly IMatcher _matcher;
//     private readonly IOutputWriter _output;

//     public CsvProcessor(
//         IConfigurationLoader configurationLoader,
//         ICsvReader csvReader,
//         IFilePath filePath,
//         IFilter filter,
//         IActionPolicy actionPolicy,
//         IMatcher matcher,
//         IOutputWriter outputWriter
//     )
//     {
//         _configuration = configurationLoader ?? throw new ArgumentNullException(nameof(configurationLoader));
//         _csvReader = csvReader ?? throw new ArgumentNullException(nameof(csvReader));
//         _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
//         _filter = filter ?? throw new ArgumentNullException(nameof(filter));
//         _policy = actionPolicy ?? throw new ArgumentNullException(nameof(actionPolicy));
//         _matcher = matcher ?? throw new ArgumentNullException(nameof(matcher));
//         _output = outputWriter ?? throw new ArgumentNullException(nameof(outputWriter));
//     }

//     public async Task<CsvMatchFilterResult> CsvInputResult(CsvMatchFilterConfig config)
//     {
//         if (config == null) throw new ArgumentNullException(nameof(config));

//         var filters = await _filter.BuildFilterAsync(config.Filter);
//         var results = new List<(string[] row, Enum.Actions action)>();

//         var files = _filePath.NormalizeFilePath(config.Input);
//         if (!files.Any())
//             throw new FileNotFoundException("No input files found.");

//         string[]? headers = null;
//         if (config.Input.HasHeader)
//         {
//             headers = await ReadHeadersAsync(files.First(), config.Input.Delimeter);
//         }

//         foreach (var path in files)
//         {
//             await foreach (var row in _csvReader.ReadFileAsync(File.OpenRead(path), config.Input.HasHeader, config.Input.Delimeter))
//             {
//                 int index = ResolveIndex(headers, config.MatchFilter.InputColumns.First());
//                 string value = row[index];

//                 bool matched = await _matcher.IsMatch(value, filters, config.MatchFilter);
//                 double similarity = matched ? 1.0 : 0.0;

//                 var action = _policy.ApplyAction(similarity, config.ActionPolicy);

//                 var finalRow = (config.ActionPolicy.MarkColumn && action == Enum.Actions.Mark)
//                     ? row.Append("MARKED").ToArray()
//                     : row;

//                 results.Add((finalRow, action));
//             }
//         }

//         await _output.WriteResultAsync(config.OutPut, results);

//         return new CsvMatchFilterResult
//         {
//             Input = config.Input,
//             Filter = config.Filter,
//             MatchFilter = config.MatchFilter,
//             Output = config.OutPut,
//             action = config.ActionPolicy,
//             Audit = config.Audit
//         };
//     }

//     private async Task<string[]> ReadHeadersAsync(string filePath, string delimiter)
//     {
//         await foreach (var headerRow in _csvReader.ReadFileAsync(File.OpenRead(filePath), true, delimiter))
//         {
//             return headerRow;
//         }
//         throw new InvalidOperationException("Header row could not be read.");
//     }

//     private int ResolveIndex(string[]? headers, string columnRef)
//     {
//         if (int.TryParse(columnRef, out var index))
//             return index;

//         if (headers == null)
//             throw new InvalidOperationException("Cannot resolve column name without headers.");

//         int resolved = Array.IndexOf(headers, columnRef);
//         if (resolved == -1)
//             throw new ArgumentException($"Column '{columnRef}' not found in headers.");
//         return resolved;
//     }
// }
