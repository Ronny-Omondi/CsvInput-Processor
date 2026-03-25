using System;
using CsvProcessor.Core.Models;

namespace CsvProcessor.Core.Interfaces;

/// <summary>
/// Defines methods for loading configuration data used by the CSV
/// filtering and matching services.
/// </summary>
public interface IConfigurationLoader
{
    /// <summary>
    /// Loads a strongly typed filter configuration from a configuration file.
    /// </summary>
    /// <param name="configurationFile">
    /// The path to the configuration file containing filter settings.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. 
    /// The task result contains a <see cref="CsvMatchFilterConfig"/> object
    /// with the parsed filter configuration.
    /// </returns>
    public Task<CsvMatchFilterConfig> LoadStinConfigurationsAsync(string configurationFile);

    /// <summary>
    /// Loads the root configuration from a stream.
    /// </summary>
    /// <param name="stream">
    /// The <see cref="Stream"/> containing configuration data.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. 
    /// The task result contains a <see cref="RootConfig"/> object
    /// with the parsed configuration.
    /// </returns>
    public Task<RootConfig> LoadConfigurations(Stream stream);
}
