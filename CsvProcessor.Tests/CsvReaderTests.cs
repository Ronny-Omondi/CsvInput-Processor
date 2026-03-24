using Xunit;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using CsvProcessor.Core.Interfaces;
using CsvProcessor.Core.Implementation;

public class CsvStreamReaderTests
{
    private readonly ICsvReader _reader;
    public CsvStreamReaderTests()
    {
        _reader = new CsvFileReader();
    }
    private async Task<List<string[]>> CollectAsync(IAsyncEnumerable<string[]> source)
    {
        var result = new List<string[]>();
        await foreach (var row in source)
        {
            result.Add(row);
        }
        return result;
    }

    [Fact]
    public async Task ReadFileAsync_WithHeader_SkipsHeaderRow()
    {
        var csv = "id,name\n1,Ronny";
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(csv));

        var rows = await CollectAsync(_reader.ReadFileAsync(stream, true, ","));

        Assert.Single(rows);
        Assert.Equal("Ronny", rows[0][1]);
    }

    [Fact]
    public async Task ReadFileAsync_WithoutHeader_IncludesHeaderRow()
    {
        var csv = "id,name\n1,Ronny";
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(csv));

        var rows = await CollectAsync(_reader.ReadFileAsync(stream, false, ","));

        Assert.Equal(2, rows.Count);
        Assert.Equal("id", rows[0][0]);
    }

    [Fact]
    public async Task ReadFileAsync_CustomDelimiter_SplitsCorrectly()
    {
        var csv = "id;name\n1;Ronny";
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(csv));

        var rows = await CollectAsync(_reader.ReadFileAsync(stream, true, ";"));

        Assert.Single(rows);
        Assert.Equal("Ronny", rows[0][1]);
    }

    [Fact]
    public async Task ReadFileAsync_EmptyStream_ReturnsEmpty()
    {
        using var stream = new MemoryStream();

        var rows = await CollectAsync(_reader.ReadFileAsync(stream, true, ","));

        Assert.Empty(rows);
    }

    [Fact]
    public async Task ReadFileAsync_NullStream_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            await foreach (var _ in _reader.ReadFileAsync(null, true, ","))
            {
                // should not reach here
            }
        });
    }
}