namespace PatioBlox2016.JobPrepUI.JobReporter
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Text.RegularExpressions;
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

      StoreListFiles = new BindableCollection<PatchFileViewModel>();
    }

    public bool CanBuildPatchList()
    {
      return true;
    }

    public void BuildPatchList()
    {
      var storeListPath = StoreListFiles.Select(f => f.FilePath).FirstOrDefault();
      _reporter.TemplatePath = Path.Combine(_jobFolders.ReportDir.FullName, "PatchReport.xlsx");
      _reporter.OutputPath = Path.Combine(_jobFolders.ReportDir.FullName, "PageCount.xlsx");

      try {
        _reporter.BuildDtoList(storeListPath);
        _reporter.BuildPatchReport();
      }
      catch (Exception exc) {
        ShowErrorWindow(exc.Message);
      }
      
    }

    public void BuildMetrixFile()
    {
      
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

      args.Handled = true;
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