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
		private DirectoryInfo _udfRoot;

		private const string IcDir = @"PartsMaster\IC";
		private const string UdfDir = "UserDefinedFolders";

    public void Initialize(string jobFolderPath)
		{
			if (!jobFolderPath.Contains(UdfDir)) {
				throw new DirectoryNotFoundException(
					string.Format("Unable to find the directory '{0}' in the path to your Excel file.", UdfDir));
			}

			_excelFileInfo = new FileInfo(Pathing.GetUncPath(jobFolderPath));

			if (!_excelFileInfo.Exists) {
				throw new FileNotFoundException();
			}

			var directories = GetDirectoriesInPath(_excelFileInfo.FullName).ToList();

			_udfRoot = directories.Find(d => d.Name == UdfDir);
      //var partsMaster = _udfRoot.GetDirectories("PartsMaster");
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
  }
}