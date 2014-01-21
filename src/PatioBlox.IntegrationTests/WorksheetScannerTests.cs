namespace PatioBlox.IntegrationTests
{
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Reflection;
	using Domain;
	using FluentAssertions;
	using NUnit.Framework;
	using DataImport;
	using NUnit.Framework.Constraints;

	[TestFixture]
	public class WorksheetScannerTests
	{
		private readonly string _testFilePath;
		private WorkSheetScanner _scanner;

		public WorksheetScannerTests()
		{
			_testFilePath = FindExcelFile();
		}

		[SetUp]
		public void TestInit()
		{
			_scanner = new WorkSheetScanner(_testFilePath);
		}

		[TestCase(1, 13)]
		[TestCase(2, 16)]
		[TestCase(3, 10)]
		[TestCase(4, 1)]
		public void WorksheetScanner_FindHeaderRow_ReturnsExpected(int sheetNum, int result)
		{
			var firstRowIndex = _scanner.FindHeaderRow(sheetNum);

			firstRowIndex.Should().Be(result);
		}

		[Test]
		[ExpectedException(typeof(InvalidDataException),
			ExpectedMessage = "Can't find header row on sheet ZB of workbook PbTestFile.xlsx")]
		public void WorksheetScanner_FindHeaderRow_NoHeaderRowInWorksheet_Throws()
		{
			var firstRowIndex = _scanner.FindHeaderRow(5);

			firstRowIndex.Should().Be(13);
		}

		[Test]
		public void CanFindTestFile()
		{
			string pth = FindExcelFile();

			File.Exists(pth).Should().BeTrue();
		}

		[TestCase(2, "5, 3, 6, 7, 8")]
		[TestCase(1, "2, 5, 7, 8, 9")]
		public void WorksheetScanner_FindColumns_ReturnsExpected(int sheetNum, string expec)
		{
			var headerRow = _scanner.FindHeaderRow(sheetNum);
			var cols = _scanner.FindColumns(sheetNum, headerRow);

			cols.ToString().Should().Be(expec);
		}

		[Test]
		[ExpectedException(typeof (InvalidDataException),
			ExpectedMessage = "Unable to find the Description column in worksheet ZW of workbook PbTestFile.xlsx")]
		public void WorksheetScanner_FindColumns_NoColumn_Throws()
		{
			var headerRow = _scanner.FindHeaderRow(4);
			var cols = _scanner.FindColumns(4, headerRow);

			cols.ToString().Should().Be("5, 7");
		}

		[Test]
		public void WorksheetScanner_FindPatches_ReturnsAllWorksheets()
		{
			const int begin = 0;
			var counter = begin;
			var expected = new List<Patch>
				{
				new Patch {Name="AB", Id = begin + 1},
				new Patch {Name = "AC", Id = begin + 2},
				new Patch {Name = "OA", Id = begin + 3},
				new Patch {Name = "ZW", Id = begin + 4},
				new Patch {Name = "ZB", Id = begin + 5}
				};

			var patches = _scanner.FindPatches(ref counter);

			patches.Should().Equal(expected);
			counter.Should().Be(5);
		}

		[Test]
		public void WorksheetScanner_FindPatches_IncrementsRefCounter()
		{
			const int begin = 0;
			var counter = begin;
			var patches = new List<Patch>();
			patches.AddRange(_scanner.FindPatches(ref counter));
			patches.AddRange(_scanner.FindPatches(ref counter));

			counter.Should().Be(begin + patches.Count);
		}

		[Test]
		public void WorksheetScanner_FindSections_ReturnsExpected()
		{
			const int begin = 0;
			var counter = begin;
			var expec = new List<string>
				{
				"RETAINING WALLS",
				"PREMIUM STONE",
				"COMMODITY STONES",
				"EDGER",
				"NATURAL"
				};

			var headerRow = _scanner.FindHeaderRow(1);
			var cols = _scanner.FindColumns(1, headerRow);
			var sections = _scanner.FindSections(1, cols.Section, headerRow, ref counter);

			sections.Select(s => s.Name).Should().Equal(expec);
			counter.Should().Be(begin + expec.Count);
		}

		[Test]
		public void WorksheetScanner_FindBlocks_FindsAllBlocksOnPatch()
		{
			var firstBlok = new PatioBlock
				{
				ItemNumber = 41288,
				Description = "FULTON 8.1-IN TAN WALL BLOCK",
				PalletQuantity = "315",
				Barcode = new Barcode("742786300584")
				};

			var lastBlok = new PatioBlock
				{
				ItemNumber = 344686,
				Description = "16-IN CALIFORNIA GOLD RUSH SLATE",
				PalletQuantity = "72",
				Barcode = new Barcode("B8904121500116")
				};

			var patchId = 1;
			var startRow = _scanner.FindHeaderRow(1);
			var cols = _scanner.FindColumns(1, startRow);
			var counter = 0;

			var blox = _scanner.FindBloxForPatch(patchId, startRow, cols, ref counter);

			blox.First().Should().Be(firstBlok);
			blox.Last().Should().Be(lastBlok);
		}

		private static string FindExcelFile()
		{
			var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
			if (directoryName == null) return null;

			var path = directoryName.Replace(@"file:\", "");

			var directoryInfo = new DirectoryInfo(path).Parent;
			if (directoryInfo == null) return null;

			var root = directoryInfo.Parent;
			if (root == null) return null;

			return Path.Combine(root.FullName, @"TestFiles\PbTestFile.xlsx");
		}
	}
}