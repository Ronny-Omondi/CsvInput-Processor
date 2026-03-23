using System;
using CsvProcessor.Core.Enum;
using CsvProcessor.Core.Models;

namespace CsvProcessor.Core.Interfaces;

public interface IFilter
{
    public Task<HashSet<string>> BuildFilterAsync(FilterData filterData);
}
