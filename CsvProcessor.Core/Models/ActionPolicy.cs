using System;

namespace CsvProcessor.Core.Models;

public class ActionPolicy
{
    public string Policy { get; set; }
    public bool MarkColumn { get; set; }
    public List<Tier> Tiers { get; set; }
}
