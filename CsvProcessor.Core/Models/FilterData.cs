using System;
using CsvProcessor.Core.Enum;
using Microsoft.AspNetCore.Http;

namespace CsvProcessor.Core.Models;

public class FilterData
{
    public List<string> FilterCsv { get; set; }
    public bool HasHeader { get; set; }
    public List<string> Columns { get; set; }
    public JoinAction JoinBy { get; set; }
    public string Delimeter { get; set; }
}
