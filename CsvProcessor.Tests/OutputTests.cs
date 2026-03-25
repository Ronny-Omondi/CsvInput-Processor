using System.IO;
using System.Threading.Tasks;
using Xunit;
using CsvProcessor.Core.Implementation;
using CsvProcessor.Core.Models;
using CsvProcessor.Core.Enum;

public class CsvOutputWriterTests
{
    [Fact]
    public async Task WriteResultAsync_WritesRowsToCorrectFiles_NoException()
    {
        // Arrange: create guaranteed non-null directory
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);

        var outputConfig = new OutPut
        {
            Dir = tempDir,
            WriteKept = "kept.csv",
            WriteRemoved = "removed.csv",
            WriteMarked = "marked.csv"
        };

        var results = new List<(string[] row, Actions action)>
    {
        (new[] { "Alice", "Seattle" }, Actions.Keep),
        (new[] { "Bob", "Nairobi" }, Actions.Remove),
        (new[] { "Charlie", "London" }, Actions.Mark)
    };

        var writer = new OutPutWriter();

        // Act
        await writer.WriteResultAsync(outputConfig, results);

        // Assert: verify file contents
        var keepLines = await File.ReadAllLinesAsync(Path.Combine(tempDir, "kept.csv"));
        var removeLines = await File.ReadAllLinesAsync(Path.Combine(tempDir, "removed.csv"));
        var markLines = await File.ReadAllLinesAsync(Path.Combine(tempDir, "marked.csv"));

        Assert.Contains("Alice,Seattle", keepLines);
        Assert.Contains("Bob,Nairobi", removeLines);
        Assert.Contains("Charlie,London", markLines);

        // Cleanup
        Directory.Delete(tempDir, true);
    }
}