using System;
using CsvProcessor.Core.Enum;

namespace CsvProcessor.Core.Models;

public class Tokenization
{
    public TokenizationMode TokenizationMode { get; set; }
    public int MinTokenLength { get; set; }
}
