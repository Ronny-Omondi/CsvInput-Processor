using Xunit;
using System.Collections.Generic;
using CsvProcessor.Core.Interfaces;
using System.Text.RegularExpressions;
using CsvProcessor.Core.Implementation;
using CsvProcessor.Core.Models;
using CsvProcessor.Core.Enum;

public class CsvMatchTests
{
    private readonly IMatcher matcher;
    public CsvMatchTests()
    {
        matcher = new Matcher();
    }
    private readonly HashSet<string> _filters = new HashSet<string> { "Ronny", "Sam" };

    [Fact]
    public async Task IsMatch_ExactMatch_ReturnsTrue()
    {
        var filters = new HashSet<string> { "Ronny" };
        var match = new Matchs { Mode = CsvProcessor.Core.Enum.Mode.Exact };

        var result = await matcher.IsMatch("Ronny", filters, match);

        Assert.True(result);
    }

    [Fact]
    public async Task IsMatch_FuzzyMatch_ReturnsTrue()
    {
        var filters = new HashSet<string> { "Ronny" };
        var match = new Matchs { Mode = Mode.Fuzzy };

        var result = await matcher.IsMatch("Ronnie", filters, match);

        Assert.True(result);
    }

    [Fact]
    public async Task IsMatch_NullInput_ReturnsFalse()
    {
        var filters = new HashSet<string> { "Ronny" };
        var match = new Matchs { Mode = Mode.Exact };

        var result = await matcher.IsMatch(null, filters, match);

        Assert.False(result);
    }

    [Fact]
    public async Task IsMatch_UnsupportedMode_ThrowsException()
    {
        var filters = new HashSet<string> { "Ronny" };
        var match = new Matchs { Mode = (Mode)3 };

        await Assert.ThrowsAsync<NotSupportedException>(async () =>
           await matcher.IsMatch("Ronny", filters, match));
    }
}
