namespace PatioBlox2016.JobPrepUI.JobReporter
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Text.RegularExpressions;
  using System.Threading.Tasks;
  using System.Windows;
  using Caliburn.Micro;
  using Abstract;
  using ViewModels;

  public class JobReporterViewModel : Screen
  {
    private readonly IWindowManager _windowManager;
    private readonly IReporter _reporter;
    private readonly IJobFolders _jobFolders;
    private BindableCollection<PatchFileViewModel> _storeListFiles;
    private bool _isInitialized;
    private string _storeListPath;

    public JobReporterViewModel(
      IWindowManager windowManager,  
      IJobFolders jobFolders, 
      IReporter reporter)
    {
      if (windowManager == null) throw new ArgumentNullException("windowManager");
      if (jobFolders == null) throw new ArgumentNullException("jobFolders");
      if (reporter == null) throw new ArgumentNullException("reporter");

      _windowManager = windowManager;
      _jobFolders = jobFolders;
      _reporter = reporter;

      IsInitialized = false;
      StoreListPath = "";
      StoreListFiles = new BindableCollection<PatchFileViewModel>();
    }

    public bool CanBuildPatchList
    {
      get { return IsInitialized; }
    }

    public string StoreListPath 
    {
      get { return _storeListPath; }
      set
      {
        if (value == _storeListPath) return;
        _storeListPath = value;
        NotifyOfPropertyChange(() => StoreListPath);
      }
    }

    public async Task BuildPatchList()
    {
      var templatePath = _jobFolders.PageCountExcelTemplatePath;
      var outputPath = _jobFolders.PageCountReportPath;

      try {
        await _reporter.BuildPatchReport(templatePath, outputPath);
      }
      catch (Exception exc) {
        ShowErrorWindow(exc.Message);
      }

      ShowMessageWindow("Excel page count file export was successfully completed");
    }

    public bool CanBuildMetrixFile
    {
      get { return IsInitialized; }
    }

    public async Task BuildMetrixFile()
    {
      try {
        await _reporter.BuildMetrixCsv(_jobFolders.MetrixCsvPath);
      }
      catch (Exception exc) {
        ShowErrorWindow(exc.Message);
      }

      ShowMessageWindow("Metrix file export was successfully completed");
    }

    private void ShowErrorWindow(string message)
    {
      var errorWindow = new ErrorWindowViewModel
      {
        DisplayName = "Warning, error encountered",
        Errors = message
      };

      _windowManager.ShowDialog(errorWindow);
    }

    private void ShowMessageWindow(string message)
    {
      var windo = new MessageWindowViewModel(message);

      _windowManager.ShowDialog(windo);
    }

    public BindableCollection<PatchFileViewModel> StoreListFiles
    {
      get { return _storeListFiles; }
      set
      {
        if (Equals(value, _storeListFiles)) return;
        _storeListFiles = value;
        NotifyOfPropertyChange(() => StoreListFiles);
      }
    }

    public bool IsInitialized 
    {
      get { return _isInitialized; }
      set
      {
        if (value == _isInitialized) return;
        _isInitialized = value;
        NotifyOfPropertyChange(() => IsInitialized);
      }
    }

    public void HandleFileDrag(object evtArgs)
    {
      var args = (DragEventArgs)evtArgs;
      args.Effects = DragDropEffects.None;

      var validPaths = GetPathFromEventArgs(args);
      if (!validPaths.Any()) return;

      args.Effects = DragDropEffects.Link;
      args.Handled = true;
    }

    public void HandleFileDrop(ActionExecutionContext ctx)
    {
      var args = (DragEventArgs)ctx.EventArgs;

      var validPaths = GetPathFromEventArgs(args).ToList();
      if (!validPaths.Any()) return;

      var storeFile = validPaths.FirstOrDefault();

      if (storeFile != null) {
        StoreListFiles.Add(new PatchFileViewModel(storeFile));
      }

      try {
        _reporter.Initialize(storeFile);
        StoreListPath = storeFile;
        IsInitialized = true;
      }
      catch (Exception exception) {
        ShowErrorWindow(exception.Message);
      }
      finally {
        args.Handled = true;
        NotifyOfPropertyChange(() => CanBuildPatchList);
        NotifyOfPropertyChange(() => CanBuildMetrixFile);
      }
    }

    private static IEnumerable<string> GetPathFromEventArgs(DragEventArgs eventArgs)
    {
      var result = new List<string>();
      var filePaths = (string[]) eventArgs.Data.GetData(DataFormats.FileDrop);

      if (!filePaths.Any()) return result;
      var info = new FileInfo(filePaths[0]);

      var isFile = (info.Attributes & FileAttributes.Directory) != FileAttributes.Directory;
      var isExcel = Regex.IsMatch(info.Extension, @"\.xlsx?$");

      if (isFile && isExcel) {
        result.Add(info.FullName);
      }

      return result;
    }
  }
}