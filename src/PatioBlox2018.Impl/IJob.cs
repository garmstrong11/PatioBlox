namespace PatioBlox2018.Impl {
  using System.Collections.Generic;

  public interface IJob
  {
    string Name { get; }
    IDictionary<string, ScanbookBook> Books { get; }
    int PageCount { get; }
    void BuildBooks(string blockPath, string storePath);
    string GetJsxBlocks();
    void EmitPatchDataFile();
  }
}