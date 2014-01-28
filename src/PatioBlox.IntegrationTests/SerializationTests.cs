namespace PatioBlox.IntegrationTests
{
	using System.IO;
	using System.Linq;
	using System.Runtime.Serialization;
	using System.Text;
	using System.Xml;
	using DataImport;
	using Domain;
	using FluentAssertions;
	using NUnit.Framework;
	using TestFiles;

	[TestFixture]
	public class SerializationTests
	{
		[Test]
		public void Serialization_Patches_Canserialize()
		{
			var testFile = TestHelpers.LocateFile("PbTestFile.xlsx");
			var scanner = new WorkSheetScanner(testFile);
			var proj = new Project();
			//const int sheetNumber = 1;

			//var startLine = scanner.FindHeaderRow(sheetNumber);
			//var cols = scanner.FindHeaderRow(sheetNumber);
			int patchCounter = 0;
			proj.Patches = scanner.FindPatches(ref patchCounter);

			var patch = new Patch
				{
				Id = 15,
				Name = "GMA"
				};

			var dcs = new DataContractSerializer(typeof (Project));

			var settings = new XmlWriterSettings {Indent = true};
			using (var xmlWriter = XmlWriter.Create(@"F:\Lowes\test3.xml", settings)) {
				dcs.WriteObject(xmlWriter, proj);
			}

			Project proj2;
			using (var reader = XmlReader.Create(@"F:\Lowes\test3.xml")) {
				proj2 = (Project) dcs.ReadObject(reader);
			}

			proj2.Patches.Should().Equal(proj.Patches);
		}
	}
}