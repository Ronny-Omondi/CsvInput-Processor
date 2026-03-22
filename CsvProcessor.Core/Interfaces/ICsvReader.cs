using System;
using CsvProcessor.Core.Models;

namespace CsvProcessor.Core.Interfaces;

public interface ICsvReader
{
    IAsyncEnumerable<string[]> ReadFileAsync(Stream stream, bool HasHeader, string Delimeter);
}
