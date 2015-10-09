namespace PatioBlox2016.JobPrepUI.JobReporter
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Text.RegularExpressions;
  using System.Windows;
  using Caliburn.Micro;
  using PatioBlox2016.Abstract;
  using PatioBlox2016.Extractor;
  using PatioBlox2016.JobPrepUI.ViewModels;

  public class JobReporterViewModel : Screen
  {
    private readonly IWindowManager _windowManager;
    private readonly IAdvertisingPatchExtractor _patchExtractor;
    private readonly IJob _job;
    private readonly IJobFolders _jobFolders;
    private string _storeListPath;

    public JobReporterViewModel(
      IWindowManager windowManager, 
      IAdvertisingPatchExtractor patchExtractor, 
      IJob job, 
      IJobFolders jobFolders)
    {
      if (windowManager == null) throw new ArgumentNullException("windowManager");
      if (patchExtractor == null) throw new ArgumentNullException("patchExtractor");
      if (job == null) throw new ArgumentNullException("job");
      if (jobFolders == null) throw new ArgumentNullException("jobFolders");

      _windowManager = windowManager;
      _patchExtractor = patchExtractor;
      _job = job;
      _jobFolders = jobFolders;
    }

    public void BuildPatchList()
    {
      
    }

    public void BuildMetrixFile()
    {
      
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
        _storeListPath = storeFile;
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