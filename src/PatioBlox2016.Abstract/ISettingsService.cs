namespace PatioBlox2016.Abstract
{
	public interface ISettingsService
	{
		int CellsPerPage { get; set; }

		string TemplateFolderName { get; set; }
		string ReportFolderName { get; set; }
		string ScriptsFolderName { get; set; }

		string InDesignOutputPath { get; set; }
		string InDesignInputPath { get; set; }
		string FactoryPath { get; set; }



		string CloverBaseAddress { get; set; }

		string BaseScriptName { get; set; }
	}
}