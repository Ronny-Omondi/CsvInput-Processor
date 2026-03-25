using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CsvProcessor.Core.Enum;
using CsvProcessor.Core.Implementation;
using CsvProcessor.Core.Interfaces;
using CsvProcessor.Core.Models;
using Moq;
using Xunit;

public class FilterTests
{
    [Fact]
    public async Task BuildFilterAsync_NullFilterData_ThrowsArgumentNullException()
    {
        var mockCsvReader = new Mock<ICsvReader>();
        var mockFilePath = new Mock<IFilePath>();
        var filter = new Filter(mockCsvReader.Object, mockFilePath.Object);

        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            filter.BuildFilterAsync(null!));
    }

    [Fact]
    public void FilterSetBuilder_Union_ReturnsCombinedValues()
    {
        var sets = new List<IEnumerable<string>>
        {
            new[] { "A", "B" },
            new[] { "B", "C" }
        };

        var filter = new Filter(Mock.Of<ICsvReader>(), Mock.Of<IFilePath>());
        var result = filter.FilterSetBuilder(sets, JoinAction.Union);
        

        Assert.Equal(3, result.Count);
        Assert.Contains("A", result);
        Assert.Contains("B", result);
        Assert.Contains("C", result);
    }

    [Fact]
    public void FilterSetBuilder_Intersect_ReturnsCommonValues()
    {
        var sets = new List<IEnumerable<string>>
        {
            new[] { "X", "Y" },
            new[] { "Y", "Z" }
        };

        var filter = new Filter(Mock.Of<ICsvReader>(), Mock.Of<IFilePath>());
        var result = filter.FilterSetBuilder(sets, JoinAction.Intersection);

        Assert.Single(result);
        Assert.Contains("Y", result);
    }

    [Fact]
    public void FilterSetBuilder_EmptySets_ReturnsEmpty()
    {
        var sets = new List<IEnumerable<string>>();

        var filter = new Filter(Mock.Of<ICsvReader>(), Mock.Of<IFilePath>());
        var result = filter.FilterSetBuilder(sets, JoinAction.Union);

        Assert.Empty(result);
    }

    [Fact]
    public void FilterSetBuilder_CaseInsensitiveUnion_WorksCorrectly()
    {
        var sets = new List<IEnumerable<string>>
        {
            new[] { "apple" },
            new[] { "APPLE" }
        };

        var filter = new Filter(Mock.Of<ICsvReader>(), Mock.Of<IFilePath>());
        var result = filter.FilterSetBuilder(sets, JoinAction.Union);

        Assert.Single(result);
        Assert.Contains("apple", result, StringComparer.OrdinalIgnoreCase);
    }

    // [Fact]
    // public async Task BuildFilterAsync_UnionMode_ReturnsValues()
    // {
    //     var mockCsvReader = new Mock<ICsvReader>();
    //     var mockFilePath = new Mock<IFilePath>();

    //     // Fake ExtractValues result: two sets
    //     var sets = new List<IEnumerable<string>>
    //     {
    //         new[] { "Ronny", "Jane" },
    //         new[] { "Jane", "Mark" }
    //     };

    //     mockCsvReader
    //         .Setup(r => r.ReadFileAsync(
    //             It.IsAny<Stream>(),
    //             It.IsAny<bool>(),
    //             It.IsAny<string>()))
    //         .ReturnsAsync(sets);

    //     var filter = new Filter(mockCsvReader.Object, mockFilePath.Object);

    //     var filterData = new FilterData
    //     {
    //         FilePath = "fake.csv",
    //         HasHeader = true,
    //         Delimeter = ",",
    //         Columns = new List<string> { "Name" },
    //         JoinBy = JoinAction.Union
    //     };

    //     var result = await filter.BuildFilterAsync(filterData);

    //     Assert.Equal(3, result.Count);
    //     Assert.Contains("Ronny", result);
    //     Assert.Contains("Jane", result);
    //     Assert.Contains("Mark", result);
    // }

    // [Fact]
    // public async Task BuildFilterAsync_IntersectMode_ReturnsCommonValues()
    // {
    //     var mockCsvReader = new Mock<ICsvReader>();
    //     var mockFilePath = new Mock<IFilePath>();

    //     var sets = new List<IEnumerable<string>>
    //     {
    //         new[] { "X", "Y" },
    //         new[] { "Y", "Z" }
    //     };

    //     mockCsvReader
    //         .Setup(r => r.ReadFileAsync(
    //             It.IsAny<Stream>(),
    //             It.IsAny<bool>(),
    //             It.IsAny<string>()))
    //         .ReturnsAsync(sets);

    //     var filter = new Filter(mockCsvReader.Object, mockFilePath.Object);

    //     var filterData = new FilterData
    //     {
    //         FilePath = "fake.csv",
    //         HasHeader = true,
    //         Delimeter = ",",
    //         Columns = new List<string> { "Col1", "Col2" },
    //         JoinBy = JoinAction.Intersection
    //     };

    //     var result = await filter.BuildFilterAsync(filterData);

    //     Assert.Single(result);
    //     Assert.Contains("Y", result);
    // }
}