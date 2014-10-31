namespace PatioBlox.Tests.MismatchResolverTests
{
	using System.Collections.Generic;
	using System.IO;
	using DataImport;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class MismatchResolverTests
	{
		private IList<string> _patioBloxPaths;
		private string _factoryDataPath;
		private string _patch1;
		private string _patch2;
		private string _correctorFilename;

		[TestFixtureSetUp]
		public void Init()
		{
			var factoryRoot = Domain.Properties.Settings.Default.FactoryRootPath;
			var dataSubPath = Domain.Properties.Settings.Default.SubpathData;
			_factoryDataPath = Path.Combine(factoryRoot, dataSubPath);

			_patch1 = "Patio Block_2015 by Patch.xlsx";
			_patch2 = "Patio Block_2015 by Patch2 v2.xlsx";
			_correctorFilename = "Mismatches Resolved.xlsx";
		}
		
		[SetUp]
		public void RunBeforeEachTest()
		{			
			_patioBloxPaths = new List<string> { _patch1, _patch2 };
		}

		[Test]
		public void GetPatchLocators_ReturnsExpected()
		{
			var resolver = new MismatchResolver {FactoryDataPath = _factoryDataPath};

			var list = resolver.GetPatchLocators(_patioBloxPaths);

			list.Count.Should().Be(217);
		}

		[Test]
		public void GetCorrectors_ReturnsExpected()
		{
			var resolver = new MismatchResolver { FactoryDataPath = _factoryDataPath };
			var locators = resolver.GetPatchLocators(_patioBloxPaths);
			var list = resolver.GetCorrectors(_correctorFilename, locators);

			list.Count.Should().Be(302);
		}

		[Test]
		public void ResolveMismatches_FixesFiles()
		{
			var resolver = new MismatchResolver { FactoryDataPath = _factoryDataPath };
			var locators = resolver.GetPatchLocators(_patioBloxPaths);
			var correctors = resolver.GetCorrectors(_correctorFilename, locators);

			resolver.ResolveMismatches(correctors);
		}
	}
}