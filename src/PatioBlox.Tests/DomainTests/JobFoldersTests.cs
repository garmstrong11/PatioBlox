namespace PatioBlox.Tests.DomainTests
{
	using Domain;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class JobFoldersTests
	{
		private const string Expected =
			@"function JobFolders() {
	var root = ""/F/Lowes/Patio Blocks 2014/factory/"";

	this.scriptPath = root + ""jsx/"";

	this.slotInddPath = root + ""output/slot/indd/"";
	this.slotPdfPath = root + ""output/slot/pdf/"";
	this.slotSupportPath = root + ""support/"";
	this.slotTemplatePath = root + ""template/slot/"";

	this.compositeInddPath = root + ""output/composite/indd/"";
	this.compositePdfPath = root + ""output/composite/pdf/"";
	this.compositeSupportPath = root + ""output/slot/pdf/"";
	this.compositeTemplatePath = root + ""template/composite/"";
}";

		[Test]
		public void JobFolders_GetJsxString_ReturnsCorrectString()
		{
			var str = JobFolders.GetJsxString();

			str.Should().Be(Expected);
		}
	}
}