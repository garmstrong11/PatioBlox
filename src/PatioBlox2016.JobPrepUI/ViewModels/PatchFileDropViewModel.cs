namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Text.RegularExpressions;
  using System.Threading.Tasks;
  using System.Windows;
  using Abstract;
  using Caliburn.Micro;
  using Concrete.Exceptions;
  using Extractor;
  using FluentValidation.Results;
  using Infra;
  using PatioBlox2016.Concrete;
  using Xceed.Wpf.Toolkit;

  public class PatchFileDropViewModel : Screen, IPatchFileDropViewModel
  {
    private readonly IJobFolders _jobFolders;
    private readonly IPatchExtractor _extractor;
    private readonly IExtractionResult _result;
    private readonly IEventAggregator _eventAggregator;
    private BindableCollection<PatchFileViewModel> _patchFiles;
    private BusyIndicator _busyIndicator;

    public PatchFileDropViewModel(IJobFolders jobFolders, 
      IPatchExtractor extractor,
      IExtractionResult result, IEventAggregator eventAggregator)
    {
      _jobFolders = jobFolders;
      _extractor = extractor;
      _result = result;
      _eventAggregator = eventAggregator;

      PatchFiles = new BindableCollection<PatchFileViewModel>();
    }

    protected override void OnViewAttached(object view, object context)
    {
      _busyIndicator = (FindBusyIndicator((FrameworkElement) view)).First();
      ExtractionProgress = 0;
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

      args.Handled = true;
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
      var validationResult = new ValidationResult();

      try {
        foreach (var patchFileViewModel in PatchFiles) {
          _extractor.Initialize(patchFileViewModel.FilePath);
          await Task.Run(() => _result.AddPatchRowExtractRange(_extractor.Extract()));
        }

        var fileInfoAdapter = new FileInfoAdapter(PatchFiles.First().FilePath);
        _jobFolders.Initialize(fileInfoAdapter);
      }

      catch (PatioBloxHeaderExtractionException exc) {
        var failure = new ValidationFailure("FlexCel Header Extraction Error", exc.Message);
        validationResult.Errors.Add(failure);
      }

      catch (FlexCelExtractionException exc) {
        var failure = new ValidationFailure("FlexCel Cell Extraction Error", exc.Message);
        validationResult.Errors.Add(failure);
      }

      catch (JobFoldersInitializationException exc) {
        var failure = new ValidationFailure("Job Folders Initialization Error", exc.Message);
        validationResult.Errors.Add(failure);
      }

      catch (Exception exc) {
        var failure = new ValidationFailure("Unknown Exception", exc.Message);
        validationResult.Errors.Add(failure);
      }

      finally {
        var acquisitionEvent = new AcquisitionCompleteEvent
        {
          RowCount = _result.PatchRowExtracts.Count(),
          ValidationResult = validationResult
        };

        _eventAggregator.PublishOnUIThread(acquisitionEvent);
        _busyIndicator.IsBusy = false;
      }
    }

    private IEnumerable<string> GetPathsFromEventArgs(DragEventArgs eventArgs)
    {
      var filePaths = (string[]) eventArgs.Data.GetData(DataFormats.FileDrop);

      return from filePath in filePaths 
             let info = new FileInfo(filePath) 
             where (info.Attributes & FileAttributes.Directory) != FileAttributes.Directory 
             where Regex.IsMatch(filePath, @"\.xlsx?$") 
             select filePath;
    }
  }
}