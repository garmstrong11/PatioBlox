namespace PatioBlox2016.Concrete
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Text;
  using Abstract;
  using Exceptions;

  [Serializable]
	public class JobFolders : IJobFolders
  {
    private readonly ISettingsService _settings;
    private IDirectoryInfoAdapter _udfDir;
    private IDirectoryInfoAdapter _icDir;
    private readonly HashSet<IDirectoryInfoAdapter> _allDirs;
    private IDirectoryInfoAdapter _pdfResultDir;
    private IDirectoryInfoAdapter _reportDir;
    private IDirectoryInfoAdapter _factoryScriptsDir;
    private IDirectoryInfoAdapter _inddDir;
    private IDirectoryInfoAdapter _supportDir;
    private IDirectoryInfoAdapter _jsxDir;
    private IDirectoryInfoAdapter _factoryDir;

    private const string UdfDir = "UserDefinedFolders";
    private const string JobInputDirName = "_Input";
    private const string BookBuilderScriptName = "BookBuilder.jsx";
    private const string BarcodeBuilderScriptName = "BarcodeBuilder.jsx";
    private const string JobDataFileName = "JobData.jsx";

    public JobFolders(ISettingsService settingsService)
    {
      if (settingsService == null) throw new ArgumentNullException("settingsService");

      _settings = settingsService;
      _allDirs = new HashSet<IDirectoryInfoAdapter>();
    }

    public void Initialize(IFileInfoAdapter excelFileAdapter)
    {
      if (excelFileAdapter == null) { throw new ArgumentNullException("excelFileAdapter");}
      if (!excelFileAdapter.Exists) { throw new ArgumentException("excelFileAdapter"); }

      _allDirs.UnionWith(GetDirectoriesInPath(excelFileAdapter));

      _udfDir = _allDirs.FirstOrDefault(d => d.Name == UdfDir);
      if (_udfDir == null) {
        throw new JobFoldersInitializationException(excelFileAdapter,
          string.Format("Unable to find '{0}' in the path to your Excel file.", UdfDir));
      }

      _factoryDir = new DirectoryInfoAdapter(_settings.PatioBloxFactoryPath);
      if (!_factoryDir.Exists) {
        throw new JobFoldersInitializationException(excelFileAdapter,
          "Unable to find the PatioBlox factory folder.");
      }

      _allDirs.UnionWith(_udfDir.GetDirectories("*.*", SearchOption.AllDirectories));

      _icDir = _allDirs.FirstOrDefault(d => d.Name == "IC");
      if (_icDir == null) {
        throw new JobFoldersInitializationException(excelFileAdapter,
          "Unable to find the 'IC' folder for this job");
      }

      JobName = _udfDir.Parent.Name;

      _reportDir = _icDir.CreateSubdirectory("reports");
      _inddDir = _icDir.CreateSubdirectory("indd");
      _jsxDir = _icDir.CreateSubdirectory("jsx");
      _pdfResultDir = _icDir.Parent.CreateSubdirectory(JobInputDirName);
      _supportDir = _factoryDir.CreateSubdirectory("support");
      _factoryScriptsDir = _factoryDir.CreateSubdirectory("scripts");
    }

    private static IEnumerable<IDirectoryInfoAdapter> GetDirectoriesInPath(IFileInfoAdapter excelAdapter)
    {
      var parentDir = excelAdapter.Directory;

      while (parentDir != null)
      {
        yield return parentDir;
        parentDir = parentDir.Parent;
      }
    }

    public IEnumerable<string> GetExistingPhotoFileNames()
    {
      if (_supportDir == null) {
        throw new DirectoryNotFoundException("Unable to find the 'Support' directory");
      }

      return _supportDir.GetFiles("*.psd", SearchOption.AllDirectories)
        .Select(f => f.NameWithoutExtension);
    }

    public void Reset()
    {
      _allDirs.Clear();
    }

    public string JobName { get; private set; }

    public string PageCountReportPath
    {
      get { return Path.Combine(_reportDir.FullName, "PageCount.xlsx"); }
    }

    public string MetrixCsvPath
    {
      get
      {
        var filename = string.Format("{0}_MetrixProducts.csv", JobName);
        return Path.Combine(_reportDir.FullName, filename);
      }
    }

    public string PageCountExcelTemplatePath
    {
      get { return Path.Combine(_factoryDir.FullName, "template", "PatchReport.xlsx"); }
    }

    public string ExtractionReportPath
    {
      get { return Path.Combine(_reportDir.FullName, "ExtractionReport.txt"); }
    }

    public string UiScriptIncludePath
    {
      get { return Path.Combine(_factoryScriptsDir.FullName, "ui.jsx"); }
    }

    public string Ean13IncludeScriptPath
    {
      get { return Path.Combine(_factoryScriptsDir.FullName, "EAN-13.jsx"); }
    }

    public string BookBuilderBaseScriptPath
    {
      get { return Path.Combine(_factoryScriptsDir.FullName, BookBuilderScriptName); }
    }

    public string BookBuilderOutputScriptPath
    {
      get { return Path.Combine(_jsxDir.FullName, BookBuilderScriptName); }
    }

    public string JobDataOutputScriptPath
    {
      get { return Path.Combine(_jsxDir.FullName, JobDataFileName); }
    }

    public string BarcodeBuilderBaseScriptPath
    {
      get { return Path.Combine(_factoryScriptsDir.FullName, BarcodeBuilderScriptName); }
    }

    public string BarcodeBuilderOutputScriptPath
    {
      get { return Path.Combine(_jsxDir.FullName, BarcodeBuilderScriptName); }
    }

    public string ToJsxString(int indentLevel)
    {
      var contentLevel = indentLevel + 1;
      var sb = new StringBuilder();

      var inddPath = string.Format("'inddPath' : '{0}/',", _inddDir.FullName);

      var outputPath = string.Format("'pdfResultPath' : '{0}/',", _pdfResultDir.FullName);

      var supportPath = string.Format("'supportPath' : '{0}/',", _supportDir.FullName);

      var templatePath = string.Format("'templatePath' : '{0}'", 
        Path.Combine(_factoryDir.FullName, "template", "book.idml"));

      var barcodeTemplatePath = string.Format("'barcodeTemplatePath' : '{0}'",
        Path.Combine(_factoryDir.FullName, "template", "barcode.idml"));

      sb.AppendLine("var jobFolders = {".Indent(indentLevel));
      sb.AppendLine(inddPath.Indent(contentLevel));
      sb.AppendLine(outputPath.Indent(contentLevel));
      sb.AppendLine(supportPath.Indent(contentLevel));
      sb.AppendLine(templatePath.Indent(contentLevel));
      sb.AppendLine(barcodeTemplatePath.Indent(contentLevel));
      sb.AppendLine("};".Indent(indentLevel));

      return sb.ToString().FlipSlashes();
    }
  }
}