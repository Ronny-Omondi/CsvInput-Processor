using System;
using CsvHelper;
using CsvProcessor.Core.Interfaces;
using CsvProcessor.Core.Models;

namespace CsvProcessor.Core.Implementation;

public class CsvProcessor : ICsvProcessor
{
    private readonly IConfigurationLoader _configuration;
    private readonly ICsvReader _csvReader;
    private readonly IFilePath _filePath;

    public CsvProcessor(
        IConfigurationLoader configurationLoader,
        ICsvReader csvReader,
        IFilePath filePath
    )
    {
        _configuration = configurationLoader;
        _csvReader = csvReader;
        _filePath = filePath;
    }

    public async Task<CsvMatchFilterResult> CsvInputResult(CsvMatchFilterConfig csvMatchFilter)
    {
        //load configuration file
        var config = await _configuration.LoadStinConfigurationsAsync("");
        //read input data
        var files = _filePath.NormalizeFilePath(config.Input);
        foreach (var file in files)
        {
            using var stream = File.OpenRead(file);
            await foreach (
                var row in _csvReader.ReadFileAsync(
                    stream,
                    config.Input.HasHeader,
                    config.Input.Delimeter
                )
            ) { }
        }
        //read filter data
        //filter
        //match
        //action
        //out put
        return new CsvMatchFilterResult();
    }
}
