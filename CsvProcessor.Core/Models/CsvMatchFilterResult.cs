using System;

namespace CsvProcessor.Core.Models;

public class CsvMatchFilterResult
{
     public InputData Input { get; set; }
    public FilterData Filter { get; set; }
    public Matchs MatchFilter { get; set; }
    public OutPut OutPut { get; set; }
    public ActionPolicy ActionPolicy { get; set; }
    public bool Audit { get; set; }
}
