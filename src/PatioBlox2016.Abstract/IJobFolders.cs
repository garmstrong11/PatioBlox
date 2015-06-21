namespace PatioBlox2016.Abstract
{
  using System.IO.Abstractions;
  using System.Threading.Tasks;

  public interface IJobFolders
  {
		string ExcelFileName { get; }
    string PartsPath { get; }
		string ReportPath { get; }
		string FontsPath { get; }
		bool FileExists(string filePath);

		FileInfoBase FileInfoFromPath(string path);
		string ServerJsxFileName { get; }

		void Initialize(string excelFilePath);
		void Reset();

		FileInfoBase GetLiftFileInfo(string liftName);
		string GetLiftName(int index, int count);

	  Task TransferPartsFilesToJob();
		//string AssembleKickScript(IEnumerable<ISignArt> signs);
		Task TransferScriptToServer(string jsx);
  }
}