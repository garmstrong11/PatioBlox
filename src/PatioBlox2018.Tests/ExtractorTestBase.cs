namespace PatioBlox2018.Tests
{
  using System.IO;
  using System.Text.RegularExpressions;
  using NUnit.Framework;

  public class ExtractorTestBase
  {
    public ExtractorTestBase()
    {
      var testDir = TestContext.CurrentContext.TestDirectory;
      DataFilesDir = Regex.Replace(testDir, @"bin\\.*$", @"\DataFiles");

      Patch1Path = Path.Combine(DataFilesDir, "Patches1.xlsx");
      Patch2Path = Path.Combine(DataFilesDir, "Patches2.xlsx");
      Patch3Path = Path.Combine(DataFilesDir, "Patches3.xlsx");
      StoreListPath = Path.Combine(DataFilesDir, "StoreList.xlsx");
    }

    protected string DataFilesDir { get; }
    protected string Patch1Path { get; }
    protected string Patch2Path { get; }
    protected string Patch3Path { get; }
    protected string StoreListPath { get; }
  }
}