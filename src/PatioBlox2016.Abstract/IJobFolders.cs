namespace PatioBlox2016.Abstract
{
  using System.Collections.Generic;
  using System.IO;

  public interface IJobFolders : IJsxExportable
  {
		string ReportPath { get; }
		bool FileExists(string filePath);

    string SupportPath { get; }

    /// <summary>
    /// Fetches a list of filename (without extension)
    /// from the support folder of the job.
    /// </summary>
    /// <returns></returns>
    IEnumerable<string> GetExistingPhotoFileNames();

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