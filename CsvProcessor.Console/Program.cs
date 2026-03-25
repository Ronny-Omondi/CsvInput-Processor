using CsvHelper;
using CsvProcessor.Core.Enum;
using CsvProcessor.Core.Implementation;
using CsvProcessor.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Quickenshtein;

public class Program
{
    static async Task Main(string[] args)
    {
        var service = new ServiceCollection();
        service.AddSingleton<IConfigurationLoader, ConfigurationLoader>();
        service.AddSingleton<ICsvReader, CsvFileReader>();
        service.AddSingleton<IFilter, Filter>();
        service.AddSingleton<IFilePath, FilePath>();
        service.AddSingleton<IMatcher, Matcher>();

        var serviceProvider = service.BuildServiceProvider();
        var config = serviceProvider.GetRequiredService<IConfigurationLoader>();
        var configRes = await config.LoadStinConfigurationsAsync(
            @"C:\Users\RONNY OMONDI\OneDrive\Desktop\CsvInput-Processor\CsvProcessor.Console\config.json"
        );

        var match = serviceProvider.GetRequiredService<IMatcher>();

        HashSet<string> filters = new HashSet<string>
        {
            "Ronny","Alice","Mark"
        };
        var filter = serviceProvider.GetRequiredService<IFilter>();

        var build =await filter.BuildFilterAsync(configRes.Filter);
        Console.WriteLine(await match.IsMatch("Ronny", filters, configRes.MatchFilter));


    }
}

public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
}
