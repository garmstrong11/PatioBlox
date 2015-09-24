namespace PatioBlox2016.Tests.ConcreteTests
{
  using System;
  using System.IO;
  using System.IO.Abstractions;
  using FakeItEasy;
  using FluentAssertions;
  using NUnit.Framework;
  using PatioBlox2016.Abstract;
  using PatioBlox2016.Concrete;

  [TestFixture]
  public class JobFoldersTests
  {
    private IFileSystem _fileSystem;
    private ISettingsService _settings;

    [TestFixtureSetUp]
    public void Init()
    {
      _settings = A.Fake<ISettingsService>();
    }

    [SetUp]
    public void RunBeforeEachTest()
    {
      _fileSystem = A.Fake<IFileSystem>();
    }

    [Test]
    public void Ctor_Throws_OnNullSettingsService()
    {
      JobFolders jobFolders;
      Action action = () => jobFolders = new JobFolders(null, _fileSystem);

      action.ShouldThrow<ArgumentNullException>();
    }

    [Test]
    public void Ctor_Throws_OnNullFileSystem()
    {
      JobFolders jobFolders;
      Action act = () => jobFolders = new JobFolders(_settings, null);

      act.ShouldThrow<ArgumentNullException>();
    }

    [Test]
    public void Initialize_NoUdfDirInPath_Throws()
    {
      var testPath = @"C:Fake Lowes US 2016 Patio Blocks\PartsMaster\IC\Test.xlsx";

      var jobFolders = new JobFolders(_settings, _fileSystem);
      Action act = () => jobFolders.Initialize(testPath);

      act.ShouldThrow<DirectoryNotFoundException>();
    }

    [Test]
    public void Initialize_ExcelFileNotFound_Throws()
    {
      A.CallTo(_fileSystem).Where(call => call.Method.Name == "get_Exists");
      NextCall.To(_fileSystem.FileInfo)
      var jobFolders = new JobFolders(_settings, _fileSystem);
      Action act = () => jobFolders.Initialize(@"C:Fake\UserDefinedFolders\PartsMaster\IC\Test.xlsx");

      act.ShouldThrow<FileNotFoundException>();
    }
  }
}