namespace PatioBlox2016.Abstract
{
  using System.IO.Abstractions;
  using System.Threading.Tasks;

  public interface IJobFolders
  {
    string PartsPath { get; }
		string ReportPath { get; }
		string FontsPath { get; }
		bool FileExists(string filePath);

		FileInfoBase FileInfoFromPath(string path);
		string ServerJsxFileName { get; }

		void Initialize(string jobFolderPath);
		void Reset();

	  //Task TransferPartsFilesToJob();
		//string AssembleKickScript(IEnumerable<ISignArt> signs);
		Task TransferScriptToServer(string jsx);
  }
}