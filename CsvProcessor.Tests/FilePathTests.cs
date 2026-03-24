using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using CsvProcessor.Core.Interfaces;
using CsvProcessor.Core.Implementation;
using CsvProcessor.Core.Models;

public class FilePathTests
{
    private readonly IFilePath _filePath;
    public FilePathTests()
    {
        _filePath=new FilePath();
    }
    [Fact]
    public void GetCsvPath_ValidFile_ReturnsPath()
    {
        var path = _filePath.GetCsvPath(@"C:\Users\RONNY OMONDI\OneDrive\Desktop\CsvInput-Processor\CsvProcessor.Tests\testdata\valid.csv");
        Assert.Equal(@"C:\Users\RONNY OMONDI\OneDrive\Desktop\CsvInput-Processor\CsvProcessor.Tests\testdata\valid.csv", path);
    }

    [Fact]
    public void GetCsvPath_InvalidExtension_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() =>
            _filePath.GetCsvPath(@"C:\Users\RONNY OMONDI\OneDrive\Desktop\CsvInput-Processor\CsvProcessor.Tests\testdata\invalid.txt"));
    }

    [Fact]
    public void GetAllCsvPath_ValidPattern_ReturnsFiles()
    {
        var files = _filePath.GetAllCsvPath("*.csv", @"C:\Users\RONNY OMONDI\OneDrive\Desktop\CsvInput-Processor\CsvProcessor.Tests\testdata");
        Assert.Contains(@"C:\Users\RONNY OMONDI\OneDrive\Desktop\CsvInput-Processor\CsvProcessor.Tests\testdata\valid.csv", files);
    }

    [Fact]
    public void GetAllCsvPath_NoMatches_ReturnsEmpty()
    {
        var files = _filePath.GetAllCsvPath("*.csv", @"C:\Users\RONNY OMONDI\OneDrive\Desktop\CsvInput-Processor\CsvProcessor.Tests\empty");
        Assert.Empty(files);
    }

    [Fact]
    public void GetMultipartPath_ValidFile_ReturnsPath()
    {
        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.FileName).Returns("upload.csv");

        var path = _filePath.GetMultipartPath(mockFile.Object);
        Assert.EndsWith(".csv", path);
    }

    [Fact]
    public void GetAllMultipartCsvPath_EmptyList_ReturnsEmpty()
    {
        var files = _filePath.GetAllMultipartCsvPath(new List<IFormFile>());
        Assert.Empty(files);
    }

    [Fact]
    public void GetMultipleCsvPath_MixedValidAndInvalid_ThrowsException()
    {
        var paths = new List<string> { @"C:\Users\RONNY OMONDI\OneDrive\Desktop\CsvInput-Processor\CsvProcessor.Tests\testdata\valid.csv", @"C:\Users\RONNY OMONDI\OneDrive\Desktop\CsvInput-Processor\CsvProcessor.Tests\testdata\invalid.txt" };
        Assert.Throws<ArgumentException>(() =>
            _filePath.GetMultipleCsvPath(paths));
    }

    [Fact]
    public void NormalizeFilePath_ValidInputData_ReturnsNormalizedPaths()
    {
        var inputData = new InputData { FilePaths = new List<string> { @"C:\Users\RONNY OMONDI\OneDrive\Desktop\CsvInput-Processor\CsvProcessor.Tests\testdata\valid.csv" } };
        var normalized = _filePath.NormalizeFilePath(inputData);
        Assert.Single(normalized);
    }
}