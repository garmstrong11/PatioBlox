namespace PatioBlox2016.Tests.ConcreteTests
{
  using System;
  using System.Collections.Generic;
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
    private IFileInfoAdapter _fileInfoAdapter;

    [TestFixtureSetUp]
    public void Init()
    {
      _settings = A.Fake<ISettingsService>();
    }

    [SetUp]
    public void RunBeforeEachTest()
    {
      _fileInfoAdapter = A.Fake<IFileInfoAdapter>();
    }

    [Test]
    public void Ctor_Throws_OnNullSettingsService()
    {
      JobFolders jobFolders;
      Action action = () => jobFolders = new JobFolders(null);

      action.ShouldThrow<ArgumentNullException>();
    }

    [Test]
    public void Initialize_NoUdfDirInPath_Throws()
    {
      var dir = A.Fake<IDirectoryInfoAdapter>();
      A.CallTo(() => dir.Name).Returns("NotUserDefinedFolders");
      A.CallTo(() => dir.Parent).Returns(null);

      A.CallTo(() => _fileInfoAdapter.Exists).Returns(true);
      A.CallTo(() => _fileInfoAdapter.Directory).Returns(dir);

      var jobFolders = new JobFolders(_settings);
      Action act = () => jobFolders.Initialize(_fileInfoAdapter);

      act.ShouldThrow<DirectoryNotFoundException>();
    }

    [Test]
    public void Initialize_ExcelFileNotFound_Throws()
    {
      A.CallTo(() => _fileInfoAdapter.Exists).Returns(false);
      var jobFolders = new JobFolders(_settings);
      Action act = () => jobFolders.Initialize(_fileInfoAdapter);

      act.ShouldThrow<ArgumentException>();
    }

    [Test]
    public void GetExistingPhotoFileNames_SupportDirNotFound_Throws()
    {
      var dirParent = A.Fake<IDirectoryInfoAdapter>();
      A.CallTo(() => dirParent.Name).Returns("UserDefinedFolders");
      A.CallTo(() => dirParent.Parent).Returns(null);

      var dirChild1 = A.Fake<IDirectoryInfoAdapter>();
      A.CallTo(() => dirChild1.Name).Returns("Blasphemy");

      var dirChild2 = A.Fake<IDirectoryInfoAdapter>();
      A.CallTo(() => dirChild2.Name).Returns("Tudors");

      var list = new List<IDirectoryInfoAdapter> {dirChild1, dirChild2};
      A.CallTo(() => dirParent.GetDirectories(A<string>._, SearchOption.AllDirectories)).Returns(list);

      A.CallTo(() => _fileInfoAdapter.Directory).Returns(dirParent);
      A.CallTo(() => _fileInfoAdapter.Exists).Returns(true);

      var jobFolder = new JobFolders(_settings);
      jobFolder.Initialize(_fileInfoAdapter);

      Action act = () => jobFolder.GetExistingPhotoFileNames();

      act.ShouldThrow<DirectoryNotFoundException>().WithMessage("*Support*");
    }

    [Test]
    public void Integration_SmokeTest()
    {
      var settings = A.Fake<ISettingsService>();
      A.CallTo(() => settings.PatioBloxFactoryPath).Returns(@"\\Storage2\AraxiVolume_1183xx\Factory\Lowes\PatioBlox");
      var xl = new FileInfoAdapter(@"C:\Users\garmstrong\Dropbox\PatioBlox\Fake Lowes US 2016 Patio Blocks\UserDefinedFolders\PartsMaster\IC\Patches1.xlsx");
      var jobFolders = new JobFolders(settings);

      jobFolders.Initialize(xl);
    }
  }
}