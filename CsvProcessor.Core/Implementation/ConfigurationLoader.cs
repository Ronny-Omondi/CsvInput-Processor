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

// using System.Text.Json;
// using YamlDotNet.Serialization;
// using YamlDotNet.Serialization.NamingConventions;

// public class ConfigurationLoader : IConfigurationLoader
// {
//     public async Task<CsvMatchFilterConfig> LoadAsync(Stream stream)
//     {
//         using var reader = new StreamReader(stream);
//         var text = await reader.ReadToEndAsync();
//         return await LoadFromStringAsync(text);
//     }

//     public async Task<CsvMatchFilterConfig> LoadFromFileAsync(string path)
//     {
//         if (!File.Exists(path))
//             throw new FileNotFoundException($"Config file not found: {path}");

//         var text = await File.ReadAllTextAsync(path);
//         return await LoadFromStringAsync(text);
//     }

//     public Task<CsvMatchFilterConfig> LoadFromStringAsync(string configText)
//     {
//         if (string.IsNullOrWhiteSpace(configText))
//             throw new ArgumentException("Configuration text cannot be empty.");

//         // Detect format
//         if (configText.TrimStart().StartsWith("{"))
//         {
//             // JSON
//             var config = JsonSerializer.Deserialize<CsvMatchFilterConfig>(configText,
//                 new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
//             return Task.FromResult(Validate(config));
//         }
//         else
//         {
//             // YAML
//             var deserializer = new DeserializerBuilder()
//                 .WithNamingConvention(CamelCaseNamingConvention.Instance)
//                 .Build();
//             var config = deserializer.Deserialize<CsvMatchFilterConfig>(configText);
//             return Task.FromResult(Validate(config));
//         }
//     }

//     private CsvMatchFilterConfig Validate(CsvMatchFilterConfig config)
//     {
//         if (config == null)
//             throw new InvalidOperationException("Failed to parse configuration.");

//         if (config.Input?.CsvPaths == null || !config.Input.CsvPaths.Any())
//             throw new InvalidOperationException("Input.CsvPaths must be provided.");

//         if (string.IsNullOrWhiteSpace(config.Output?.Dir))
//             throw new InvalidOperationException("Output.Dir must be provided.");

//         return config;
//     }
// }

// static async Task Main(string[] args)
// {
//     Console.WriteLine("Enter configuration file path (JSON or YAML):");
//     var path = Console.ReadLine();

//     var loader = new ConfigurationLoader();
//     var config = await loader.LoadFromFileAsync(path);

//     Console.WriteLine("CSV match filter started...");
//     var processor = new CsvProcessor(/* dependencies */);
//     await processor.CsvInputResult(config);
// }

// [HttpPost("process")]
// public async Task<IActionResult> Process([FromForm] IFormFile configFile)
// {
//     using var stream = configFile.OpenReadStream();
//     var loader = new ConfigurationLoader();
//     var config = await loader.LoadAsync(stream);

//     var processor = new CsvProcessor(/* dependencies */);
//     var result = await processor.CsvInputResult(config);

//     return Ok(result);
// }
