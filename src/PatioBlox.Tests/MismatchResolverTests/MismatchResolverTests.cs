namespace PatioBlox.Tests.MismatchResolverTests
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using DataImport;
	using FakeItEasy;
	using FlexCel.XlsAdapter;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class MismatchResolverTests
	{
		private XlsFile _xls;
		private IList<string> _patioBloxPaths;
		private string _factoryDataPath;
		private string _patch1;
		private string _patch2;
		private string _correctorPath;

		[TestFixtureSetUp]
		public void Init()
		{
			var factoryRoot = Domain.Properties.Settings.Default.FactoryRootPath;
			var dataSubPath = Domain.Properties.Settings.Default.SubpathData;
			_factoryDataPath = Path.Combine(factoryRoot, dataSubPath);

			_patch1 = "Patio Block_2015 by Patch.xlsx";
			_patch2 = "Patio Block_2015 by Patch2 v2.xlsx";
		}
		
		[SetUp]
		public void RunBeforeEachTest()
		{
			_xls = new XlsFile();
			
			_patioBloxPaths = new List<string>
				{
				Path.Combine(_factoryDataPath, _patch1),
				Path.Combine(_factoryDataPath, _patch2)
				};

			_correctorPath = Path.Combine(_factoryDataPath, "Mismatches Resolved.xlsx");
		}

		[Test]
		public void Ctor_NullPathList_Throws()
		{
			Action act = () => new MismatchResolver(null, "foo");
			act.ShouldThrow<ArgumentNullException>().WithMessage("*filePaths*");
		}

		[Test]
		public void Ctor_PathListContainsNullItems_Throws()
		{
			var list = new List<string> {"foo", ""};
			Action act = () => new MismatchResolver(list, "foo");
			act.ShouldThrow<ArgumentException>().WithMessage("*Empty*");
		}

		[Test]
		public void Ctor_NullCorrectorPath_Throws()
		{
			var list = new List<string> {"Oink", "Moo"};
			Action act = () => new MismatchResolver(list, "");

			act.ShouldThrow<ArgumentNullException>().WithMessage("*correctorPath*");
		}

		[Test]
		public void Ctor_CanConstruct()
		{
			var resolver = new MismatchResolver(_patioBloxPaths, _correctorPath);

			_patioBloxPaths.All(File.Exists).Should().BeTrue();
			File.Exists(_correctorPath).Should().BeTrue();
		}

		[Test]
		public void GetPatchNameDictForFile_ReturnsExpected()
		{
			var resolver = new MismatchResolver(_patioBloxPaths, _correctorPath);

			var dict = resolver.GetPatchNameDict();

			dict.Count.Should().Be(200);
		}
	}
}