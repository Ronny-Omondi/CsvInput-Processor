using System;
using Microsoft.AspNetCore.Http;

namespace CsvProcessor.Core.Models;

public class InputData
{
    //csv settings
    public bool HasHeader { get; set; }
    public string Delimeter { get; set; }

    //multipart files
    public List<IFormFile> Files { get; set; }
    public IFormFile File { get; set; }

    //multiple files
    public List<string> FilePaths { get; set; }
    public string FilePath { get; set; }

    //folder and glob
    public string SearchDir { get; set; }
    public string Pattern { get; set; }
}
