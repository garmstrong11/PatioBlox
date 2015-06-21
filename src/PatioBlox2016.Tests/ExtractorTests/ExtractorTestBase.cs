namespace PatioBlox2016.Tests.ExtractorTests
{
  using System.IO;
  using System.Text.RegularExpressions;
  using FluentAssertions.Common;
  using NUnit.Framework;

  public class ExtractorTestBase
  {
    public ExtractorTestBase()
    {
      var testDir = TestContext.CurrentContext.TestDirectory;
      DataFilesDir = Regex.Replace(testDir, @"bin\\.*$", @"ExtractorTests\DataFiles");

      Patch1Path = Path.Combine(DataFilesDir, "Patches1.xlsx");
      Patch2Path = Path.Combine(DataFilesDir, "Patches2.xlsx");
    }

    protected string DataFilesDir { get; private set; }
    protected string Patch1Path { get; private set; }
    protected string Patch2Path { get; private set; }
  }
}