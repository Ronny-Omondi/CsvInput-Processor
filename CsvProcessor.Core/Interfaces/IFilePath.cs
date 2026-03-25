using System;
using CsvProcessor.Core.Models;
using Microsoft.AspNetCore.Http;

namespace CsvProcessor.Core.Interfaces;

/// <summary>
/// Defines methods for resolving, normalizing, and retrieving file paths
/// for CSV files and multipart uploads.
/// </summary>
public interface IFilePath
{
    /// <summary>
    /// Retrieves all CSV file paths that match a given search pattern within a specified directory.
    /// </summary>
    /// <param name="pattern">
    /// The search pattern (e.g., "*.csv") used to filter files.
    /// </param>
    /// <param name="searchDir">
    /// The directory path in which to search for CSV files.
    /// </param>
    /// <returns>
    /// A list of file paths to CSV files that match the search criteria.
    /// </returns>
    List<string> GetAllCsvPath(string pattern, string searchDir);

    /// <summary>
    /// Extracts CSV file paths from a collection of uploaded multipart form files.
    /// </summary>
    /// <param name="uploadedFiles">
    /// A list of <see cref="IFormFile"/> objects representing uploaded files.
    /// </param>
    /// <returns>
    /// A list of file paths corresponding to the uploaded CSV files.
    /// </returns>
    List<string> GetAllMultipartCsvPath(List<IFormFile> uploadedFiles);

    /// <summary>
    /// Validates and returns multiple CSV file paths from a provided list of file paths.
    /// </summary>
    /// <param name="filePaths">
    /// A list of file path strings to be processed.
    /// </param>
    /// <returns>
    /// A list of valid CSV file paths.
    /// </returns>
    List<string> GetMultipleCsvPath(List<string> filePaths);

    /// <summary>
    /// Generates a file path for a single multipart form file.
    /// </summary>
    /// <param name="file">
    /// The <see cref="IFormFile"/> representing the uploaded file.
    /// </param>
    /// <returns>
    /// A string containing the file path for the uploaded CSV file.
    /// </returns>
    string GetMultipartPath(IFormFile file);

    /// <summary>
    /// Normalizes and validates a single CSV file path.
    /// </summary>
    /// <param name="filePath">
    /// The raw file path string to be normalized.
    /// </param>
    /// <returns>
    /// A string containing the normalized CSV file path.
    /// </returns>
    string GetCsvPath(string filePath);

    /// <summary>
    /// Normalizes file paths based on the provided input data.
    /// </summary>
    /// <param name="inputData">
    /// The <see cref="InputData"/> object containing file path information.
    /// </param>
    /// <returns>
    /// A list of normalized file paths extracted from the input data.
    /// </returns>
    List<string> NormalizeFilePath(InputData inputData);
}
