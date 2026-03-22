using System;

namespace CsvProcessor.Core.Models;

public class Encode
{
    public bool Detect { get; set; }
    public List<string> ExcludeCharClasses { get; set; }
}
