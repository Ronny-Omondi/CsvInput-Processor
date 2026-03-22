using System;
using System.Text.Json;
using CsvProcessor.Core.Interfaces;
using CsvProcessor.Core.Models;

namespace CsvProcessor.Core.Implementation;

public class ConfigurationLoader : IConfigurationLoader
{
    public async Task<CsvMatchFilterConfig> LoadStinConfigurationsAsync(string configurationFile)
    {
        if (string.IsNullOrWhiteSpace(configurationFile))
            throw new ArgumentNullException("Configuration cannot be null or empty");

        if (!File.Exists(configurationFile))
            throw new FileNotFoundException(
                "Configuration file not found",
                nameof(configurationFile)
            );

        using (var reader = new FileStream(configurationFile, FileMode.Open))
        {
            return await JsonSerializer.DeserializeAsync<CsvMatchFilterConfig>(reader)
                ?? throw new InvalidOperationException("Failed to deserialize");
        }
    }

    public Task<RootConfig> LoadConfigurations(Stream stream)
    {
        throw new NotImplementedException();
    }
}
