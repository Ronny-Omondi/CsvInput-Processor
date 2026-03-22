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

        using var streamReader = new StreamReader(stream);

        using var csvReader = new CsvReader(streamReader, config);

        if (HasHeader)
            await csvReader.ReadAsync();

        while (await csvReader.ReadAsync())
        {
            var row = csvReader.Parser.Record;
            if (row == null)
                continue;
            yield return row;
        }
    }
}
