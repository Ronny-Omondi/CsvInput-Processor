using CsvHelper;
using CsvProcessor.Core.Enum;
using CsvProcessor.Core.Implementation;
using CsvProcessor.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    static async Task Main(string[] args)
    {
        var service = new ServiceCollection();
        service.AddSingleton<IConfigurationLoader, ConfigurationLoader>();
        service.AddSingleton<ICsvReader, CsvFileReader>();
        service.AddSingleton<IFilter, Filter>();
        service.AddSingleton<IFilePath, FilePath>();

        var serviceProvider = service.BuildServiceProvider();
        var config = serviceProvider.GetRequiredService<IConfigurationLoader>();
        var configRes = await config.LoadStinConfigurationsAsync(
            @"C:\Users\Admin\Desktop\CsvProcessor\CsvProcessor.Console\config.json"
        );

        //filter
        var filter = serviceProvider.GetRequiredService<IFilter>();
        List<List<string>> sets =
        [
            new List<string> { "A", "B", "C" },
            new List<string> { "D", "B" },
            new List<string> { "A", "K" },
        ];

        var set = await filter.FilterSetBuilder(sets, JoinAction.Union);

        var file = serviceProvider.GetRequiredService<IFilePath>();

        var filePath = file.NormalizeFilePath(configRes.Input);

        var reader = serviceProvider.GetRequiredService<ICsvReader>();

        foreach (var f in filePath)
        {
            using var stream = File.OpenRead(f);
            await foreach (
                var row in reader.ReadFileAsync(
                    stream,
                    configRes.Input.HasHeader,
                    configRes.Input.Delimeter
                )
            )
            {
                System.Console.WriteLine(string.Join(" | ", row));
            }
        }
    }
}

public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
}
