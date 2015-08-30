namespace PatioBlox2016.Abstract
{
  using System.IO;

  public interface IJobFolders
  {
		string ReportPath { get; }
		bool FileExists(string filePath);

    /// <summary>
    /// Creates a FileInfo object from a given path.
    /// Delegates to System.IO.FileInfo class
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    FileInfo FileInfoFromPath(string path);

		void Initialize(string jobFolderPath);
		void Reset();
  }
}