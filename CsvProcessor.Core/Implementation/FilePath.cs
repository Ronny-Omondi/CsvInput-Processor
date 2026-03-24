using System;
using CsvProcessor.Core.Interfaces;
using CsvProcessor.Core.Models;
using Microsoft.AspNetCore.Http;

namespace CsvProcessor.Core.Implementation;

public class FilePath : IFilePath
{
    public List<string> GetAllCsvPath(string pattern, string searchDir)
    {
        if (string.IsNullOrWhiteSpace(pattern))
        {
            throw new ArgumentNullException("Pattern cannot be null or empty");
        }
        if (string.IsNullOrWhiteSpace(searchDir))
        {
            throw new ArgumentNullException("Search directory cannot be null or empty");
        }
        if (!Directory.Exists(searchDir))
        {
            throw new DirectoryNotFoundException("Directory does not exist");
        }

        var files = Directory.GetFiles(searchDir, pattern, SearchOption.TopDirectoryOnly).Where(f => string.Equals(Path.GetExtension(f), ".csv", StringComparison.OrdinalIgnoreCase)).ToList();

        return files;
    }

    public List<string> GetMultipleCsvPath(List<string> filePaths)
    {
        if (filePaths == null || !filePaths.Any())
        {
            throw new ArgumentNullException("File paths cannot be null or empty");
        }

        foreach (var file in filePaths)
        {
            if (!File.Exists(file))
            {
                throw new FileNotFoundException("Csv file not found", nameof(file));
            }
            if (!string.Equals(Path.GetExtension(file), ".csv", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("File extension must .csv");
            }
        }
        return filePaths;
    }

    public List<string> GetAllMultipartCsvPath(List<IFormFile> uploadedFiles)
    {
        var files = new List<string>();

        if (uploadedFiles == null)
        {
            throw new ArgumentNullException("Files cannot be null");
        }
    
        foreach (var file in uploadedFiles)
        {
            if (!string.Equals(Path.GetExtension(file.FileName), ".csv", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("File extension must be .csv");
            var temp = Path.Combine(Path.GetTempPath(), file.FileName);

            using (var stream = new FileStream(temp, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            files.Add(temp);
        }

        return files;
    }

    public string GetMultipartPath(IFormFile file)
    {
        if (file == null)
            throw new ArgumentNullException("File cannot be null", nameof(file));

        if (!string.Equals(Path.GetExtension(file.FileName), ".csv", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("File extension must be .csv");
        var temp = Path.Combine(Path.GetTempPath(), file.FileName);
        using (var stream = new FileStream(temp, FileMode.Create))
        {
            file.CopyTo(stream);
        }
        return temp;
    }

    public string GetCsvPath(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentNullException("File path not found", nameof(filePath));

        if (!File.Exists(filePath))
            throw new FileNotFoundException("Csv file not found", nameof(filePath));
        if (!string.Equals(Path.GetExtension(filePath), ".csv", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("File extension must be .csv");

        return filePath;
    }

    public List<string> NormalizeFilePath(InputData inputData)
    {
        if (inputData == null)
            throw new ArgumentNullException("Input data cannot be null");

        if (inputData.Files != null && inputData.Files.Any())
            return GetAllMultipartCsvPath(inputData.Files);

        if (inputData.File != null)
            return new List<string> { GetMultipartPath(inputData.File) };

        if (inputData.FilePath != null)
            return new List<string> { GetCsvPath(inputData.FilePath) };

        if (inputData.FilePaths != null && inputData.FilePaths.Count >= 1)
            return GetMultipleCsvPath(inputData.FilePaths);

        if (
            !string.IsNullOrWhiteSpace(inputData.SearchDir)
            && !string.IsNullOrWhiteSpace(inputData.Pattern)
        )
            return GetAllCsvPath(inputData.Pattern, inputData.Delimeter);

        throw new InvalidOperationException("Invalid input configuration");
    }
}
