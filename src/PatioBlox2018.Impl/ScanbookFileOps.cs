namespace PatioBlox2018.Impl
{
  using System;
  using System.Configuration;
  using System.IO;
  using PatioBlox2018.Core;

  public class ScanbookFileOps : IFileOps
  {
    public ScanbookFileOps()
    {
      PatioBloxExcelFilePath = 
        VerifyFilePath(ConfigurationManager.AppSettings["BlocksFilename"]);

      StoreListExcelFilePath = 
        VerifyFilePath(ConfigurationManager.AppSettings["StoresFilename"]);
    }

    public string PatioBloxExcelFilePath { get; }
    public string StoreListExcelFilePath { get; }

    private string VerifyFilePath(string path)
    {
      if (string.IsNullOrWhiteSpace(path))
        throw new ArgumentException("Value cannot be null or whitespace.", nameof(path));

      if (!File.Exists(path))
        throw new FileNotFoundException($"Unable to find the file '{path}'.");

      return path;
    }
  }
}