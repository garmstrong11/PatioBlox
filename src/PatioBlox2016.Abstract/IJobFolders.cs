namespace PatioBlox2016.Abstract
{
  using System.Collections.Generic;

  public interface IJobFolders : IJsxExportable
  {
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
    /// The file system destination for the page count Excel report
    /// </summary>
    string PageCountReportPath { get; }

    /// <summary>
    /// The file system destination for the Metrix product import csv file
    /// </summary>
    string MetrixCsvPath { get; }

    /// <summary>
    /// The file system source path of the page count excel report template
    /// </summary>
    string PageCountExcelTemplatePath { get; }

    /// <summary>
    /// The file system destination for the extraction validation report
    /// </summary>
    string ExtractionReportPath { get; }
    
    /// <summary>
    /// The file system source path of the Indesign javascript UI include file
    /// </summary>
    string UiScriptIncludePath { get; }

    /// <summary>
    /// The file system source path of the Indesign javascript Barcode
    /// translation include file
    /// </summary>
    string Ean13IncludeScriptPath { get; }

    /// <summary>
    /// The file system source path of the InDesign javascript
    /// book builder base file
    /// </summary>
    string BookBuilderBaseScriptPath { get; }

    /// <summary>
    /// The file system destination path for the InDesign javascript
    /// book builder output file
    /// </summary>
    string BookBuilderOutputScriptPath { get; }

    /// <summary>
    /// The file system destination path for the InDesign javascript
    /// job data file
    /// </summary>
    string JobDataOutputScriptPath { get; }

    /// <summary>
    /// The file system source path of the InDesign javascript
    /// barcode builder base file
    /// </summary>
    string BarcodeBuilderBaseScriptPath { get; }

    /// <summary>
    /// The file system destination path of the InDesign javascript
    /// barcode builder output file
    /// </summary>
    string BarcodeBuilderOutputScriptPath { get; }

    void Initialize(IFileInfoAdapter jobFolderPath);
		void Reset();
  }
}