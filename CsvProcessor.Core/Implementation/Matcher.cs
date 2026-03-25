using System;
using CsvProcessor.Core.Interfaces;
using CsvProcessor.Core.Models;
using Quickenshtein;

namespace CsvProcessor.Core.Implementation;

public class Matcher : IMatcher
{
    public async Task<bool> IsMatch(string input, HashSet<string> filters, Matchs match)
    {
        if (match == null)
            throw new ArgumentNullException("Match config must be provided", nameof(match));
        if (filters == null)
            return false;
        if (string.IsNullOrWhiteSpace(input))
            return false;
        //normalize
        //tokenize
        //stop list filtering
        //encoding
        //matching
        //column weights
        //action policy
        return match.Mode switch
        {
            Enum.Mode.Exact => ExactMatch(input, filters),
            Enum.Mode.Fuzzy => FuzzyMatch(input, filters),
            _ => throw new NotSupportedException()
        };
    }

    private double LevenshteinDistance(string input, string target)
    {
        return Levenshtein.GetDistance(input, target);
    }

    private double Similarity(double distance, string input, string target)
    {
        return 1 - (double)distance / Math.Max(input.Length, target.Length);
    }

    private bool ExactMatch(string input, HashSet<string> filters)
    {
        if (string.IsNullOrWhiteSpace(input)) return false;
        return filters.Contains(input);
    }

    private bool FuzzyMatch(string input, HashSet<string> filters)
    {
        foreach (var target in filters)
        {
            var score = Similarity(LevenshteinDistance(input, target), input, target);
            if (score >= 0.65)
                return true;
        }
        return false;
    }
}
