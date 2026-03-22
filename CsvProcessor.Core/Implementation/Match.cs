using System;
using CsvProcessor.Core.Interfaces;

namespace CsvProcessor.Core.Implementation;

public class Match : IMatcher
{
    public Task<bool> IsMatch(string input, HashSet<string> filters)
    {
        throw new NotImplementedException();
    }
}
