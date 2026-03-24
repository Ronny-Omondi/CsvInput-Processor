using System;

namespace CsvProcessor.Core.Models;

public class RootConfig
{
    public Matchs MatchFilter { get; set; }
    public OutPut OutPut { get; set; }
    public ActionPolicy ActionPolicy { get; set; }
    public bool Audit { get; set; }
}
