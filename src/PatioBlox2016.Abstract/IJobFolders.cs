namespace PatioBlox2016.Abstract
{
  using System.Collections.Generic;
  using System.IO;

  public interface IJobFolders : IJsxExportable
  {
		DirectoryInfo ReportPath { get; }
		bool FileExists(string filePath);

    string SupportPath { get; }

    /// <summary>
    /// Fetches a list of filename (without extension)
    /// from the support folder of the job.
    /// </summary>
    /// <returns></returns>
    IEnumerable<string> GetExistingPhotoFileNames();

    /// <summary>
    /// The path for output pdfs.
    /// </summary>
    string OutputPath { get; }

    /// <summary>
    /// The Job's Home path
    /// </summary>
    string JobRootPath { get; }

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