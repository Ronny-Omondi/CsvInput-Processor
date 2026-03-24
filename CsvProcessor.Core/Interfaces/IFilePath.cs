using System;
using CsvProcessor.Core.Models;
using Microsoft.AspNetCore.Http;

namespace CsvProcessor.Core.Interfaces;

public interface IFilePath
{
    List<string> GetAllCsvPath(string pattern, string searchDir);
    List<string> GetAllMultipartCsvPath(List<IFormFile> uploadedFiles);
    List<string> GetMultipleCsvPath(List<string> filePaths);
    string GetMultipartPath(IFormFile file);
    string GetCsvPath(string filePath);
    List<string> NormalizeFilePath(InputData inputData);
}
