namespace PatioBlox2016.Concrete
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using Abstract;

  [Serializable]
	public class JobFolders : IJobFolders
  {
    private readonly ISettingsService _settings;
    private FileInfo _excelFileInfo;
    private DirectoryInfo _udfRoot;
    private List<DirectoryInfo> _allUdfDirs;
    private DirectoryInfo _icDir;
    private DirectoryInfo _supportDir;

    private const string UdfDir = "UserDefinedFolders";

    public JobFolders(ISettingsService settingsService)
    {
      if (settingsService == null) throw new ArgumentNullException("settingsService");

      _settings = settingsService;
    }

    public void Initialize(string excelFilePath)
    {
      if (!excelFilePath.Contains(UdfDir))
      {
        throw new DirectoryNotFoundException(
          string.Format(
            "Unable to find the directory '{0}' in the path to your Excel file.", UdfDir));
      }

      _excelFileInfo = new FileInfo(Pathing.GetUncPath(excelFilePath));

      if (!_excelFileInfo.Exists)
        throw new FileNotFoundException(
          string.Format("Unable to locate the Excel file\n{0}", _excelFileInfo.FullName));

      if (!Directory.Exists(_settings.PatioBloxFactoryPath))
        throw new DirectoryNotFoundException("Unable to locate the PatioBlox Factory Directory.");

      var directories = GetDirectoriesInPath(_excelFileInfo.FullName).ToList();

      _udfRoot = directories.Find(d => d.Name == UdfDir);

      // We can't count on the name of the PartsMaster folder, so we find named children:
      _allUdfDirs = _udfRoot.GetDirectories("*.*", SearchOption.AllDirectories).ToList();

      _icDir = _allUdfDirs.Find(d => d.Name == "IC");
      _supportDir = _allUdfDirs.Find(d => d.Name == "Support");
    }

    public IEnumerable<DirectoryInfo> GetDirectoriesInPath(string filepath)
    {
      var fileInfo = new FileInfo(filepath);
      var parentDir = fileInfo.Directory;

      while (parentDir != null)
      {
        yield return parentDir;
        parentDir = parentDir.Parent;
      }
    } 


    public IEnumerable<string> GetExistingPhotoFileNames()
    {
      throw new NotImplementedException();
    }

    public string OutputPath
    {
      get { return Path.Combine(_udfRoot.FullName, "_Output"); } 
      
    }

    public FileInfo FileInfoFromPath(string path)
    {
      return new FileInfo(path);
    }

    public void Reset()
		{
			_excelFileInfo = new FileInfo(@"C:\waka\waka\jub\jub\bing.xlsx");
		}


    private void CheckInit()
		{
			if (!_excelFileInfo.Exists) {
				throw new InvalidOperationException("JobFolders is not initialized");
			}
		}

		public string JobRootPath 
    {
			get
			{
				CheckInit();
				return _udfRoot.FullName;
			}
    }

    //private string JobName
    //{
    //  get
    //  {
    //    if (_udfRoot == null || _udfRoot.Parent == null) return "Unknown Job";
    //    return _udfRoot.Parent.Name;
    //  }
    //}

    

		public DirectoryInfo ReportPath
	  {
		  get { return _icDir.CreateSubdirectory("reports"); }
	  }

		public bool FileExists(string filePath)
		{
			return File.Exists(filePath);
		}

    public string SupportPath
    {
      get { return Path.Combine(_udfRoot.FullName, "Support"); }
    }

    public string ToJsxString(int indentLevel)
    {
      throw new NotImplementedException();
    }
  }
}