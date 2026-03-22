using System;
using CsvProcessor.Core.Enum;

namespace CsvProcessor.Core.Models;

public class Tier
{
    public double MinThreshold { get; set; }
    public Actions Action { get; set; }
}
