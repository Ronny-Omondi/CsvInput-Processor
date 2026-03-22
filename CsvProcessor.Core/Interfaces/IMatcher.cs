using System;

namespace CsvProcessor.Core.Interfaces;

public interface IMatcher
{
    Task<bool> IsMatch(string input, HashSet<string> filters);
}
