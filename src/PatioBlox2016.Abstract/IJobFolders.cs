namespace PatioBlox2016.Abstract
{
  using System.Collections.Generic;
  using System.IO.Abstractions;

  public interface IJobFolders : IJsxExportable
  {
		DirectoryInfoBase ReportPath { get; }
		bool FileExists(string filePath);

    string SupportPath { get; }

    /// <summary>
    /// Fetches a list of filenames (without extension)
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

		void Initialize(string jobFolderPath);
		void Reset();
  }
}