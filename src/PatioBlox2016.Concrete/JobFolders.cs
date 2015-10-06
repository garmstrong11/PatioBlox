namespace PatioBlox2016.Concrete
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Text;
  using Abstract;
  using Concrete.Exceptions;

  [Serializable]
	public class JobFolders : IJobFolders
  {
    private readonly ISettingsService _settings;
    private IDirectoryInfoAdapter _udfDir;
    private IDirectoryInfoAdapter _icDir;
    private readonly HashSet<IDirectoryInfoAdapter> _allDirs; 

    private const string UdfDir = "UserDefinedFolders";

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

      FactoryDir = new DirectoryInfoAdapter(_settings.PatioBloxFactoryPath);
      if (!FactoryDir.Exists) {
        throw new JobFoldersInitializationException(excelFileAdapter,
          "Unable to find the PatioBlox factory folder.");
      }

      _allDirs.UnionWith(_udfDir.GetDirectories("*.*", SearchOption.AllDirectories));

      _icDir = _allDirs.FirstOrDefault(d => d.Name == "IC");
      if (_icDir == null) {
        throw new JobFoldersInitializationException(excelFileAdapter,
          "Unable to find the 'IC' folder for this job");
      }

      //SupportDir = _allDirs.FirstOrDefault(d => d.Name == "Support");
      //if (SupportDir == null) {
      //    throw new JobFoldersInitializationException(excelFileAdapter,
      //      "Unable to find the 'Support' folder for this job");
      //}

      JobName = _udfDir.Parent.Name;
      OutputDir = _udfDir.CreateSubdirectory("_Output");

      ReportDir = _icDir.CreateSubdirectory("reports");
      InddDir = _icDir.CreateSubdirectory("indd");
      JsxDir = _icDir.CreateSubdirectory("jsx");
      SupportDir = FactoryDir.CreateSubdirectory("support");
      FactoryScriptsDir = FactoryDir.CreateSubdirectory("scripts");
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
      if (SupportDir == null) {
        throw new DirectoryNotFoundException("Unable to find the 'Support' directory");
      }

      return SupportDir.GetFiles("*.psd", SearchOption.AllDirectories)
        .Select(f => f.NameWithoutExtension);
    }

    public void Reset()
    {
      _allDirs.Clear();
    }

    public string JobName { get; private set; }

    public IDirectoryInfoAdapter OutputDir { get; private set; }

    public IDirectoryInfoAdapter FactoryDir { get; private set; }

    public IDirectoryInfoAdapter JsxDir { get; private set; }

    public IDirectoryInfoAdapter ReportDir { get; private set; }

    public IDirectoryInfoAdapter SupportDir { get; private set; }

    public IDirectoryInfoAdapter InddDir { get; private set; }

    public IDirectoryInfoAdapter FactoryScriptsDir { get; private set; }

    public string ToJsxString(int indentLevel)
    {
      var contentLevel = indentLevel + 1;
      var sb = new StringBuilder();

      var inddPath = string.Format("'inddPath' : '{0}/',", InddDir.FullName.FlipSlashes());

      var outputPath = string.Format("'outputPath' : '{0}/',", OutputDir.FullName.FlipSlashes());

      var supportPath = string.Format("'supportPath' : '{0}/',", SupportDir.FullName.FlipSlashes());

      var templatePath = string.Format("'templatePath' : '{0}'", 
        Path.Combine(FactoryDir.FullName, "template", "book.idml")).FlipSlashes();

      sb.AppendLine("var jobFolders = {".Indent(indentLevel));
      sb.AppendLine(inddPath.Indent(contentLevel));
      sb.AppendLine(outputPath.Indent(contentLevel));
      sb.AppendLine(supportPath.Indent(contentLevel));
      sb.AppendLine(templatePath.Indent(contentLevel));
      sb.AppendLine("};".Indent(indentLevel));

      return sb.ToString();
    }
  }
}