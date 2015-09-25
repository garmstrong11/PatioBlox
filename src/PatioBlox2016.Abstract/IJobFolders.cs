namespace PatioBlox2016.Abstract
{
  using System.Collections.Generic;

  public interface IJobFolders : IJsxExportable
  {
		IDirectoryInfoAdapter ReportPath { get; }
		bool FileExists(string filePath);

    string SupportPath { get; }

    /// <summary>
    /// Fetches a list of filenames (without extension)
    /// from the support folder of the job.
    /// </summary>
    /// <returns></returns>
    IEnumerable<IFileInfoAdapter> GetExistingPhotoFiles();

    /// <summary>
    /// The path for output pdfs.
    /// </summary>
    string OutputPath { get; }

    /// <summary>
    /// The Job's Home path
    /// </summary>
    string JobRootPath { get; }

    void Initialize(IFileInfoAdapter jobFolderPath);
		void Reset();
  }
}