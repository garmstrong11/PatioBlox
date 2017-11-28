namespace PatioBlox2016.Extractor
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using PatioBlox2018.Core;

  public abstract class ExtractorBase<T> : IExtractor<T> where T : class
  {
    protected readonly IDataSourceAdapter XlAdapter;

    protected ExtractorBase(IDataSourceAdapter adapter)
    {
      XlAdapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
    }

    protected void Initialize(string path)
    {
      if (string.IsNullOrWhiteSpace(path))
        throw new ArgumentNullException(nameof(path));

      if (!File.Exists(path))
        throw new FileNotFoundException($"The file '{path}' could not be found.");

      SourcePath = path;
      XlAdapter.Open(path);
    }

    protected string SourcePath { get; private set; }
    public abstract IEnumerable<T> Extract(string excelFilePath);
  }
}