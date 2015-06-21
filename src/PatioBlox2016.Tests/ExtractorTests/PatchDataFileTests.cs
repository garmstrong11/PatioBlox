namespace PatioBlox2016.Tests.ExtractorTests
{
  using System;
  using System.CodeDom;
  using System.IO;
  using System.Linq;
  using Abstract;
  using Extractor;
  using FakeItEasy;
  using FluentAssertions;
  using NUnit.Framework;

  [TestFixture]
  public class PatchDataFileTests
  {
    private IJobFolders _jobFolders;

    [SetUp]
    public void RunBeforeEachTest()
    {
      _jobFolders = A.Fake<IJobFolders>();
    }

    [Test]
    public void NewObject_SheetNames_IsEmpty()
    {
      var patchDataFile = new PatchDataFile(_jobFolders);

      patchDataFile.SheetNames.Any().Should().BeFalse();
    }
    
    [Test]
    public void AddPatchName_PatchNameAlreadyExists_Throws()
    {
      const string newSheetName = "GA";
      var patchDataFile = new PatchDataFile(_jobFolders);
      patchDataFile.AddSheetName(newSheetName);

      Action act = () => patchDataFile.AddSheetName(newSheetName);

      act.ShouldThrow<InvalidOperationException>().WithMessage("*already exists.");
    }

    [Test]
    public void SetPath_PathDoesNotExist_Throws()
    {
      A.CallTo(() => _jobFolders.FileExists(A<string>._)).Returns(false);
      var patchDataFile = new PatchDataFile(_jobFolders);

      Action act = () => patchDataFile.Path = "booboo";
      act.ShouldThrow<FileNotFoundException>();
    }
  }
}