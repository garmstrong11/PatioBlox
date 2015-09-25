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
    private IDirectoryInfoAdapter _udfRoot;
    private IDirectoryInfoAdapter _icDir;
    private IDirectoryInfoAdapter _supportDir;
    private readonly HashSet<IDirectoryInfoAdapter> _allDirs; 
    private IDirectoryInfoAdapter _patioBloxFactoryDirInfo;

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

      _udfRoot = _allDirs.FirstOrDefault(d => d.Name == UdfDir);

      if (_udfRoot == null) {
        throw new DirectoryNotFoundException(
          string.Format("Unable to find '{0}' in the path to your Excel file.", UdfDir));
      }

      _allDirs.UnionWith(_udfRoot.GetDirectories("*.*", SearchOption.AllDirectories));
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

    public IEnumerable<IFileInfoAdapter> GetExistingPhotoFiles()
    {
      var supportDir = _allDirs.FirstOrDefault(d => d.Name == "Support");
      if (supportDir == null) {
        throw new DirectoryNotFoundException("Unable to find the 'Support' directory");
      }

      return supportDir.GetFiles("*.psd", SearchOption.AllDirectories);
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
			_allDirs.Clear();
		}

		public string JobRootPath 
    {
			get
			{
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

    

		public IDirectoryInfoAdapter ReportPath
	  {
		  get { return _icDir.CreateSubdirectory("reports"); }
	  }

		public bool FileExists(string filePath)
		{
			return File.Exists(filePath);
		}

    public string SupportPath
    {
      get { return _supportDir.FullName; }
    }

    public string ToJsxString(int indentLevel)
    {
      throw new NotImplementedException();
    }
  }
}