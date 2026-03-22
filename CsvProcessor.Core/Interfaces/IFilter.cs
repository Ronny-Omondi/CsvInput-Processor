using System;
using CsvProcessor.Core.Enum;

namespace CsvProcessor.Core.Interfaces;

public interface IFilter
{
    public Task<HashSet<string>> FilterSetBuilder(
        IEnumerable<IEnumerable<string>> sets,
        JoinAction mode
    );
}
