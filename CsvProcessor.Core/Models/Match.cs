using System;
using CsvProcessor.Core.Enum;

namespace CsvProcessor.Core.Models;

public class Match
{
    public List<string> InputColumns { get; set; }
    public Mode Mode { get; set; }
    public bool CaseSensitive { get; set; }
    public NormalizeType Normalize { get; set; }
    public List<string> StopList { get; set; }
    public Tokenization Tokenize { get; set; }
    public Encode Encoding { get; set; }

}
