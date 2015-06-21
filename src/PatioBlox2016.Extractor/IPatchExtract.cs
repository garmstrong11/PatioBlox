namespace PatioBlox2016.Extractor
{
  using System.Collections.Generic;

  /// <summary>
  /// A container for data extracted from a patch Excel worksheet
  /// </summary>
  
  public interface IPatchExtract
  {
    /// <summary>
    /// The patch's source file path
    /// </summary>
    string SourceFilePath { get; }

    string Name { get; }
  }
}