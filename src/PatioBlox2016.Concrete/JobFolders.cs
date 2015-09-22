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
		private FileInfo _excelFileInfo;
    private string _jobFolderPath;
    private string _icPath;
    private List<string> _allPaths; 
		private string _udfPath;

		private const string IcDir = @"PartsMaster\IC";
		private const string UdfDir = "UserDefinedFolders";

    public void Initialize(string jobFolderPath)
    {
      _jobFolderPath = Pathing.GetUncPath(jobFolderPath);
      _udfPath = Path.Combine(_jobFolderPath, UdfDir);

      if (!Directory.Exists(_udfPath)) {
        throw new DirectoryNotFoundException(
          string.Format("Unable to find a '{0}' directory in your job folder.", UdfDir));
      }

      _allPaths = Directory
        .EnumerateDirectories(_jobFolderPath, "*.*", SearchOption.AllDirectories)
        .ToList();

      // Need IC\data\excel for data files
      // IC\indd for generated InDesign files
      // IC\data\jsx for jsx data files
      // UserDefinedFolders\_Output for generated pdf files
      // Factory\templates
    }

    public IEnumerable<DirectoryInfo> GetDirectoriesInPath(string filepath)
		{
			var fileInfo = new FileInfo(filepath);
			var parentDir = fileInfo.Directory;

			while (parentDir != null) {
				yield return parentDir;
				parentDir = parentDir.Parent;
			}
		}


    public IEnumerable<string> GetExistingPhotoFileNames()
    {
      throw new NotImplementedException();
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

		public string ExcelFileName
		{
			get
			{
				CheckInit();
				return Path.GetFileNameWithoutExtension(_excelFileInfo.FullName);
			}
		}

		private string JobRootPath 
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

		public string ReportPath
	  {
		  get { return Path.Combine(JobRootPath, IcDir); }
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