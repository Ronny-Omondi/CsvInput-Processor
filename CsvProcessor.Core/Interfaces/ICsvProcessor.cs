using System;
using CsvProcessor.Core.Models;

namespace CsvProcessor.Core.Interfaces;

public interface ICsvProcessor
{
    public Task<CsvMatchFilterResult> CsvInputResult(CsvMatchFilterConfig csvMatchFilter);
}
