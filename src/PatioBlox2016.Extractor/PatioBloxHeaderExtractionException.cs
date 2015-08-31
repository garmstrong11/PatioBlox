namespace PatioBlox2016.Extractor
{
  using System;
  using System.IO;

  public class PatioBloxHeaderExtractionException : Exception
  {
    public PatioBloxHeaderExtractionException(string sheetName, string filePath)
    {
      SheetName = sheetName;
      FilePath = filePath;
    }

    public string SheetName { get; private set; }

    public string FilePath { get; private set; }

    public override string Message
    {
      get
      {
        var fileName = Path.GetFileName(FilePath);
        return string.Format("Unable to find a header row for patch '{0}' in file '{1}'", SheetName, fileName);
      }
    }
  }
}