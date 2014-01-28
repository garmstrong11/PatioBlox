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
	using TestFiles;

	[TestFixture]
	public class WorksheetScannerTests
	{
		private readonly string _testFilePath;
		//private WorkSheetScanner scanner;

		public WorksheetScannerTests()
		{
			_testFilePath = TestHelpers.LocateFile(@"PbTestFile.xlsx");
		}

		[SetUp]
		public void TestInit()
		{
			//_scanner = new WorkSheetScanner(_testFilePath);
		}

		[TestCase(1, 13)]
		[TestCase(2, 16)]
		[TestCase(3, 10)]
		public void WorksheetScanner_FindHeaderRow_ReturnsExpected(int sheetNum, int result)
		{
			var testFile = TestHelpers.LocateFile(@"PbTestFile.xlsx");
			var scanner = new WorkSheetScanner(testFile);
			var firstRowIndex = scanner.FindHeaderRow(sheetNum);

			firstRowIndex.Should().Be(result);
		}

		[Test]
		[ExpectedException(typeof(InvalidDataException),
			ExpectedMessage = "Can't find header row on sheet ZB of workbook NoHeaderRow.xlsx")]
		public void WorksheetScanner_FindHeaderRow_NoHeaderRowInWorksheet_Throws()
		{
			var testFile = TestHelpers.LocateFile("NoHeaderRow.xlsx");
			var scanner = new WorkSheetScanner(testFile);

			var firstRowIndex = scanner.FindHeaderRow(1);

			firstRowIndex.Should().Be(13);
		}

		[Test]
		public void CanFindTestFile()
		{
			string pth = TestHelpers.LocateFile(@"PbTestFile.xlsx");

			File.Exists(pth).Should().BeTrue();
		}

		[TestCase(2, "5, 4, 6, 7, 2")]
		[TestCase(1, "2, 5, 7, 8, 9")]
		public void WorksheetScanner_FindColumns_ReturnsExpected(int sheetNum, string expec)
		{
			var testFile = TestHelpers.LocateFile(@"PbTestFile.xlsx");
			var scanner = new WorkSheetScanner(testFile);

			var headerRow = scanner.FindHeaderRow(sheetNum);
			var cols = scanner.FindColumns(sheetNum, headerRow);

			cols.ToString().Should().Be(expec);
		}

		[Test]
		[ExpectedException(typeof (InvalidDataException),
			ExpectedMessage = "Unable to find the Description column in worksheet ZW of workbook NoDescriptionHeader.xlsx")]
		public void WorksheetScanner_FindColumns_NoColumn_Throws()
		{
			var testFile = TestHelpers.LocateFile("NoDescriptionHeader.xlsx");
			var scanner = new WorkSheetScanner(testFile);
			const int sheetIndex = 1;
			
			var headerRow = scanner.FindHeaderRow(sheetIndex);
			var cols = scanner.FindColumns(sheetIndex, headerRow);

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
				new Patch {Name = "OA", Id = begin + 3}
				};

			var testFile = TestHelpers.LocateFile(@"PbTestFile.xlsx");
			var scanner = new WorkSheetScanner(testFile);

			var patches = scanner.FindPatches(ref counter);

			patches.Should().Equal(expected);
			counter.Should().Be(expected.Count);
		}

		[Test]
		public void WorksheetScanner_FindPatches_IncrementsRefCounter()
		{
			const int begin = 0;
			var counter = begin;
			var patches = new List<Patch>();

			var testFile = TestHelpers.LocateFile(@"PbTestFile.xlsx");
			var scanner = new WorkSheetScanner(testFile);

			patches.AddRange(scanner.FindPatches(ref counter));
			patches.AddRange(scanner.FindPatches(ref counter));

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

			var testFile = TestHelpers.LocateFile(@"PbTestFile.xlsx");
			var scanner = new WorkSheetScanner(testFile);

			var headerRow = scanner.FindHeaderRow(1);
			var cols = scanner.FindColumns(1, headerRow);
			var sections = scanner.FindSections(1, cols.Section, headerRow, ref counter);

			sections.Select(s => s.Name).Should().Equal(expec);
			counter.Should().Be(begin + expec.Count);
		}

		[Test]
		public void WorksheetScanner_FindBlocks_FindsAllBlocksOnPatch()
		{
			var firstBlok = new Product
				{
				ItemNumber = 41288,
				Description = "FULTON 8.1-IN TAN WALL BLOCK",
				PalletQuantity = "315",
				Barcode = new Barcode("742786300584")
				};

			var lastBlok = new Product
				{
				ItemNumber = 344686,
				Description = "16-IN CALIFORNIA GOLD RUSH SLATE",
				PalletQuantity = "72",
				Barcode = new Barcode("B8904121500116")
				};

			const int patchId = 1;

			var testFile = TestHelpers.LocateFile(@"PbTestFile.xlsx");
			var scanner = new WorkSheetScanner(testFile);

			var startRow = scanner.FindHeaderRow(patchId);
			var cols = scanner.FindColumns(patchId, startRow);
			var counter = 0;

			var blox = scanner.FindBloxForPatch(patchId, startRow, cols, ref counter);

			blox.First().ItemNumber.Should().Be(firstBlok.ItemNumber);
			blox.Last().ItemNumber.Should().Be(lastBlok.ItemNumber);
		}
	}
}