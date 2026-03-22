using System;
using CsvProcessor.Core.Models;

namespace CsvProcessor.Core.Interfaces;

public interface IConfigurationLoader
{
    public Task<CsvMatchFilterConfig> LoadStinConfigurationsAsync(string configurationFile);
    public Task<RootConfig> LoadConfigurations(Stream stream);
}
