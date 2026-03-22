using System;
using CsvProcessor.Core.Enum;

namespace CsvProcessor.Core.Models;

public class NormalizeType
{
    public bool WhiteSpace { get; set; }
    public bool RepeatedSpaces { get; set; }
    public Unicode Code { get; set; }
    public bool Diatrics { get; set; }
    public bool CaseSensitive { get; set; }
}
