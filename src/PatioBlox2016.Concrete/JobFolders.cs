namespace PatioBlox2016.Concrete
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.IO.Abstractions;
  using System.Linq;
  using Abstract;

  [Serializable]
	public class JobFolders : IJobFolders
  {
    private readonly ISettingsService _settings;
    private readonly IFileSystem _fileSystem;
    private FileInfoBase _excelFileInfo;
    private DirectoryInfoBase _udfRoot;
    private List<DirectoryInfoBase> _allUdfDirs;
    private DirectoryInfoBase _icDir;
    private DirectoryInfoBase _supportDir;

    private const string UdfDir = "UserDefinedFolders";

    public JobFolders(ISettingsService settingsService, IFileSystem fileSystem)
    {
      if (settingsService == null) throw new ArgumentNullException("settingsService");
      if (fileSystem == null) throw new ArgumentNullException("fileSystem");

      _settings = settingsService;
      _fileSystem = fileSystem;
    }

    public void Initialize(string excelFilePath)
    {
      if (!excelFilePath.Contains(UdfDir))
      {
        throw new DirectoryNotFoundException(
          string.Format(
            "Unable to find the directory '{0}' in the path to your Excel file.", UdfDir));
      }

      _excelFileInfo = _fileSystem.FileInfo.FromFileName(Pathing.GetUncPath(excelFilePath));

      if (!_excelFileInfo.Exists)
        throw new FileNotFoundException(
          string.Format("Unable to locate the Excel file\n{0}", _excelFileInfo.FullName));

      if (!_fileSystem.Directory.Exists(_settings.PatioBloxFactoryPath))
        throw new DirectoryNotFoundException("Unable to locate the PatioBlox Factory Directory.");

      var directories = GetDirectoriesInPath(_excelFileInfo.FullName).ToList();

      _udfRoot = directories.Find(d => d.Name == UdfDir);

      // We can't count on the name of the PartsMaster folder, so we find named children:
      _allUdfDirs = _udfRoot.GetDirectories("*.*", SearchOption.AllDirectories).ToList();

      _icDir = _allUdfDirs.Find(d => d.Name == "IC");
      _supportDir = _allUdfDirs.Find(d => d.Name == "Support");
    }

    private IEnumerable<DirectoryInfoBase> GetDirectoriesInPath(string filepath)
    {
      var fileInfo = _fileSystem.FileInfo.FromFileName(filepath);
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
      get { return _fileSystem.Path.Combine(_udfRoot.FullName, "_Output"); } 
      
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

    

		public DirectoryInfoBase ReportPath
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