using System;
using CsvProcessor.Core.Interfaces;
using CsvProcessor.Core.Models;
using Quickenshtein;

namespace CsvProcessor.Core.Implementation;

public class Matcher : IMatcher
{
    public Task<bool> IsMatch(string input, HashSet<string> filters, Match match)
    {
        throw new NotImplementedException();
    }

    private double LevenshteinDistance(string input, string target)
    {
        return Levenshtein.GetDistance(input, target);
    }

    private double Similarity(double distance, string input, string target)
    {
        return 1 - (double)distance / Math.Max(input.Length, target.Length);
    }
}
