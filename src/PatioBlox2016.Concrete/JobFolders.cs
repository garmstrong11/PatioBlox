namespace PatioBlox2016.Concrete
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.IO.Abstractions;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using Abstract;

  [Serializable]
	public class JobFolders : IJobFolders
  {
		private readonly string _timeStamp;
		private readonly IFileSystem _fileSystem;
		private readonly ISettingsService _settings;
		private FileInfoBase _excelFileInfo;
		private DirectoryInfoBase _udfRoot;

		private const string SoftProofsDir = "_Softproofs";
		private const string OutputDir = "_Output";
		private const string IcDir = @"PartsMaster\IC";
		private const string UdfDir = "UserDefinedFolders";

		public JobFolders(IFileSystem fileSystem, ISettingsService settingsService)
		{
			if (fileSystem == null) throw new ArgumentNullException("fileSystem");
			if (settingsService == null) throw new ArgumentNullException("settingsService");

			_timeStamp = DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss");
			_fileSystem = fileSystem;
			_settings = settingsService;
		}

		public void Initialize(string jobFolderPath)
		{
			if (!jobFolderPath.Contains(UdfDir)) {
				throw new DirectoryNotFoundException(
					string.Format("Unable to find the directory '{0}' in the path to your Excel file.", UdfDir));
			}

			_excelFileInfo = _fileSystem.FileInfo.FromFileName(Pathing.GetUncPath(jobFolderPath));

			if (!_excelFileInfo.Exists) {
				throw new FileNotFoundException();
			}

			var directories = GetDirectoriesInPath(_excelFileInfo.FullName).ToList();

			_udfRoot = directories.Find(d => d.Name == UdfDir);
		}

		public IEnumerable<DirectoryInfoBase> GetDirectoriesInPath(string filepath)
		{
			var fileInfo = _fileSystem.FileInfo.FromFileName(filepath);
			var parentDir = fileInfo.Directory;

			while (parentDir != null) {
				yield return parentDir;
				parentDir = parentDir.Parent;
			}
		} 

		/// <summary>
		/// Generate a destination path for a pdf lift file
		/// </summary>
		/// <param name="liftName">the name of the lift.</param>
		/// <returns></returns>
		public FileInfoBase GetLiftFileInfo(string liftName)
		{
			var fileName = string.Format("{0}.pdf", liftName);

			var fullPath = _fileSystem.Path.Combine(LiftsPath, fileName);
			return _fileSystem.FileInfo.FromFileName(fullPath);
		}

		/// <summary>
		/// Generate a name for a lift.
		/// </summary>
		/// <param name="index">The one-based index of the lift.</param>
		/// <param name="count">The count of lifts in the job.</param>
		/// <returns></returns>
		public string GetLiftName(int index, int count)
		{
			var characterCount = count.ToString();
			var padLength = characterCount.Length.ToString();

			return string.Format("{0} Lift {1} of {2}", JobName, index.ToString("D" + padLength), characterCount);
		}

		public void Reset()
		{
			_excelFileInfo = _fileSystem.FileInfo.FromFileName(@"C:\waka\waka\jub\jub\bing.xlsx");
		}

		private void CopyTemplateFontsToJob()
		{
			var fontsDirName = _fileSystem.Path.GetFileName(FontsPath);
			var destinationDirName = _fileSystem.Path.Combine(IcPath, fontsDirName);
			var sourceFontFiles = _fileSystem.Directory.EnumerateFiles(FontsPath, "*.*", SearchOption.TopDirectoryOnly);

			if (!_fileSystem.Directory.Exists(destinationDirName)) {
				_fileSystem.Directory.CreateDirectory(destinationDirName);
			}

			foreach (var file in sourceFontFiles) {
				var fileName = _fileSystem.Path.GetFileName(file);
				var destName = _fileSystem.Path.Combine(destinationDirName, fileName);

				_fileSystem.File.Copy(file, destName, true);
			}
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
				return _fileSystem.Path.GetFileNameWithoutExtension(_excelFileInfo.FullName);
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

		private string JobName
		{
			get { return _udfRoot.Parent.Name; }
		}

    public string PartsPath
    {
	    get { return _fileSystem.Path.Combine(JobRootPath, SoftProofsDir); }
    }

		public string ReportPath
	  {
		  get { return _fileSystem.Path.Combine(JobRootPath, IcDir); }
	  }

		private string IcPath
	  {
			get { return _fileSystem.Path.Combine(JobRootPath, IcDir); }
	  }

		private string LiftsPath
	  {
		  get { return _fileSystem.Path.Combine(JobRootPath, OutputDir); }
	  }

		private string BaseFileName
		{
			get { return string.Format("{0}_{1}", ExcelFileName, _timeStamp); }
		}

		public bool FileExists(string filePath)
		{
			return _fileSystem.File.Exists(filePath);
		}

		public FileInfoBase FileInfoFromPath(string path)
		{
			return _fileSystem.FileInfo.FromFileName(path);
		}

		private string ServerPdfFileName
		{
			get { return _fileSystem.Path.Combine(_settings.InDesignOutputPath, BaseFileName.AddExtension("pdf")); }
		}

		private string ServerInddFileName
		{
			get { return _fileSystem.Path.Combine(_settings.InDesignOutputPath, BaseFileName.AddExtension("indd")); }
		}

		public string ServerJsxFileName
		{
			get { return _fileSystem.Path.Combine(_settings.InDesignInputPath, BaseFileName.AddExtension("jsx")); }
		}

		public string FontsPath
		{
			get { return _fileSystem.Path.Combine(_settings.FactoryPath, "Document fonts"); }
		}

		public async Task TransferPartsFilesToJob()
		{
			var icJsxFileName = _fileSystem.Path.Combine(IcPath, BaseFileName.AddExtension("jsx"));
			var icInddFileName = _fileSystem.Path.Combine(IcPath, BaseFileName.AddExtension("indd"));
			var softProofPdfFileName = _fileSystem.Path.Combine(PartsPath, BaseFileName.AddExtension("pdf"));

			using (var pdfSource = _fileSystem.File.OpenRead(ServerPdfFileName)) {
				using (var pdfDest = _fileSystem.File.Create(softProofPdfFileName)) {
					await pdfSource.CopyToAsync(pdfDest);
				}
			}

			using (var inddSource = _fileSystem.File.OpenRead(ServerInddFileName)) {
				using (var inddDest = _fileSystem.File.Create(icInddFileName)) {
					await inddSource.CopyToAsync(inddDest);
				}
			}

			using (var jsxSource = _fileSystem.File.OpenRead(ServerJsxFileName)) {
				using (var jsxDest = _fileSystem.File.Create(icJsxFileName)) {
					await jsxSource.CopyToAsync(jsxDest);
				}
			}

			_fileSystem.File.Delete(ServerPdfFileName);
			_fileSystem.File.Delete(ServerInddFileName);
			_fileSystem.File.Delete(ServerJsxFileName);

			_fileSystem.Directory.CreateDirectory(LiftsPath);

			CopyTemplateFontsToJob();
		}

    //public string AssembleKickScript(IEnumerable<ISignArt> signs)
    //{
    //  var sb = new StringBuilder();
    //  var signsJson = string.Join(",\n\t", signs.Select(s => s.ToJsonString()));

    //  var baseScriptFilePath = _fileSystem.Path.Combine(
    //    _settings.PetcoFactoryPath, _settings.BaseScriptName)
    //    .FlipSlashes();

    //  var templatePath = _fileSystem.Path.Combine(
    //    _settings.PetcoFactoryPath, "template.idml")
    //    .FlipSlashes();

    //  sb.AppendFormat("#include \"{0}\";\n\n", baseScriptFilePath);

    //  sb.AppendFormat("var outputInDesignFile = File('{0}');\n", ServerInddFileName.FlipSlashes());

    //  sb.AppendFormat("var outputPdfFile = File('{0}');\n", ServerPdfFileName.FlipSlashes());

    //  sb.AppendFormat("var templateFile = File('{0}');\n\n", templatePath.FlipSlashes());

    //  sb.AppendFormat("var signs = [\n\t{0}\n];\n\n", signsJson);

    //  sb.AppendLine("buildParts();");

    //  return sb.ToString();
    //}

		public async Task TransferScriptToServer(string jsx)
		{
			if (String.IsNullOrWhiteSpace(jsx)) throw new ArgumentNullException("jsx");

			var encodedText = Encoding.UTF8.GetBytes(jsx);

			using (var stream = _fileSystem.File.OpenWrite(ServerJsxFileName))
			{
				await stream.WriteAsync(encodedText, 0, encodedText.Length);
			}
		}
  }
}