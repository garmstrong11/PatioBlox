﻿namespace PatioBlox2016.Abstract
{
  using System.Collections.Generic;

  public interface IJobFolders : IJsxExportable
  {
		/// <summary>
		/// A destination container for job reports
		/// </summary>
    IDirectoryInfoAdapter ReportDir { get; }

    /// <summary>
    /// A destination container for output files.
    /// </summary>
    IDirectoryInfoAdapter OutputDir { get; }

    /// <summary>
    /// A source container for required art.
    /// </summary>
    IDirectoryInfoAdapter SupportDir { get; }

    /// <summary>
    /// A destination container for InDesign files.
    /// </summary>
    IDirectoryInfoAdapter InddDir { get; }

    /// <summary>
    /// A destination container for jsx data files.
    /// </summary>
    IDirectoryInfoAdapter JsxDir { get; }

    /// <summary>
    /// A source container for templates and base scripts.
    /// </summary>
    IDirectoryInfoAdapter FactoryDir { get; }

    /// <summary>
    /// Fetches a list of filenames (without extension)
    /// from the support folder of the job.
    /// </summary>
    /// <returns></returns>
    IEnumerable<string> GetExistingPhotoFileNames();

    /// <summary>
    /// The Job's Home path
    /// </summary>
    string JobName { get; }

    /// <summary>
    /// A Source container for script include files
    /// </summary>
    IDirectoryInfoAdapter FactoryScriptsDir { get; }

    void Initialize(IFileInfoAdapter jobFolderPath);
		void Reset();
  }
}