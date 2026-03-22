using System;

namespace CsvProcessor.Core.Models;

public class OutPut
{
    public string Dir { get; set; }
    public string WriteMarked { get; set; }
    public string WriteKept { get; set; }
    public string WriteRemoved { get; set; }
}

