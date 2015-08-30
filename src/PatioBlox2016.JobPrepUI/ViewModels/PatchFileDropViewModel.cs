namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Text.RegularExpressions;
  using System.Threading.Tasks;
  using System.Windows;
  using Abstract;
  using Caliburn.Micro;
  using Extractor;
  using Xceed.Wpf.Toolkit;

  public class PatchFileDropViewModel : Screen, IPatchFileDropViewModel
  {
    private readonly IJobFolders _jobFolders;
    private readonly IPatchExtractor _extractor;
    private readonly IExtractionResult _result;
    private BindableCollection<PatchFileViewModel> _patchFiles;
    private BusyIndicator _busyIndicator;

    public PatchFileDropViewModel(IJobFolders jobFolders, 
      IPatchExtractor extractor,
      IExtractionResult result)
    {
      _jobFolders = jobFolders;
      _extractor = extractor;
      _result = result;

      PatchFiles = new BindableCollection<PatchFileViewModel>();
    }

    protected override void OnViewAttached(object view, object context)
    {
      _busyIndicator = (FindBusyIndicator((FrameworkElement) view)).First();
      ExtractionProgress = 0;
      //WonkyError();
    }

    private static Maybe<BusyIndicator> FindBusyIndicator(FrameworkElement view)
    {
      var indicator = view.FindName("BusyIndicator") as BusyIndicator;
      return indicator != null ? new Maybe<BusyIndicator>(indicator) : new Maybe<BusyIndicator>();
    } 


    public void HandleFileDrag(object evtArgs)
    {
      var args = (DragEventArgs) evtArgs;
      args.Effects = DragDropEffects.None;

      var validPaths = GetPathsFromEventArgs(args);
      if (!validPaths.Any()) return;

      args.Effects = DragDropEffects.Link;
      args.Handled = true;
    }

    public void HandleFileDrop(ActionExecutionContext ctx)
    {
      var args = (DragEventArgs) ctx.EventArgs;

      var validPaths = GetPathsFromEventArgs(args).ToList();
      if (!validPaths.Any()) return;

      var patchFiles = validPaths.Select(p => new PatchFileViewModel(p));
      PatchFiles.AddRange(patchFiles);
    }

    public int ExtractionProgress { get; set; }
    public string Status { get; set; }

    public BindableCollection<PatchFileViewModel> PatchFiles
    {
      get { return _patchFiles; }
      set
      {
        if (Equals(value, _patchFiles)) return;
        _patchFiles = value;
        NotifyOfPropertyChange(() => PatchFiles);
      }
    }

    public async Task AcquirePatches()
    {
      _busyIndicator.BusyContent = "Extracting patches...";
      _busyIndicator.IsBusy = true;

      foreach (var patchFileViewModel in PatchFiles) {
        _extractor.Initialize(patchFileViewModel.FilePath);
        await Task.Run(() => _result.AddPatchRowExtractRange(_extractor.Extract()));
      }

      _busyIndicator.IsBusy = false;
    }

    private IEnumerable<string> GetPathsFromEventArgs(DragEventArgs eventArgs)
    {
      var filePaths = (string[]) eventArgs.Data.GetData(DataFormats.FileDrop);

      return from filePath in filePaths 
             let info = _jobFolders.FileInfoFromPath(filePath) 
             where (info.Attributes & FileAttributes.Directory) != FileAttributes.Directory 
             where Regex.IsMatch(filePath, @"\.xlsx?$") 
             select filePath;
    }
  }
}