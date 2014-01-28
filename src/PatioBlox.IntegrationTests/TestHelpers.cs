namespace PatioBlox.IntegrationTests.TestFiles
{
	using System.IO;
	using System.Reflection;

	public static class TestHelpers
	{
		public static string LocateFile(string fileName)
		{
			var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
			if (directoryName == null) return null;

			var path = directoryName.Replace(@"file:\", "");

			var directoryInfo = new DirectoryInfo(path).Parent;
			if (directoryInfo == null) return null;

			var root = directoryInfo.Parent;
			if (root == null) return null;

			return Path.Combine(root.FullName, "TestFiles", fileName);
		}
	}
}