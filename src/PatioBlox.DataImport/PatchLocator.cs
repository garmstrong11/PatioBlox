namespace PatioBlox.DataImport
{
	public class PatchLocator
	{
		public string FileName  { get; private set; }
		public string PatchName { get; private set; }
		public int PatchIndex   { get; private set; }

		public PatchLocator(string fileName, string patchName, int patchIndex)
		{
			FileName = fileName;
			PatchName = patchName;
			PatchIndex = patchIndex;
		}
	}
}