using System;
using CsvProcessor.Core.Enum;
using CsvProcessor.Core.Interfaces;
using CsvProcessor.Core.Models;

namespace CsvProcessor.Core.Implementation;

public class OutPutWriter : IOutputWriter
{
    public async Task WriteResultAsync(OutPut output, IEnumerable<(string[] row, Actions action)> results)
    {

        // Ensure output directory exists
        Directory.CreateDirectory(output.Dir);

        foreach (var (row, action) in results)
        {
            string filePath = action switch
            {
                Actions.Keep => Path.Combine(output.Dir, output.WriteKept ?? "kept.csv"),
                Actions.Remove => Path.Combine(output.Dir, output.WriteRemoved ?? "removed.csv"),
                Actions.Mark => Path.Combine(output.Dir, output.WriteMarked ?? "marked.csv"),
                _ => Path.Combine(output.Dir, "unknown.csv")
            };

            // Convert row to CSV line
            string line = string.Join(",", row);

            // Append line to the correct file
            await File.AppendAllTextAsync(filePath, line + Environment.NewLine);
        }
    }


}
