namespace PatioBlox2016.JobPrepUI.JobVisualizer
{
  using System;
  using System.Collections.ObjectModel;
  using System.IO;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using Caliburn.Micro;
  using Abstract;
  using JobBuilders;
  using Concrete;
  using ViewModels;

  public class JobVisualizerViewModel : Screen
  {
    private readonly IWindowManager _windowManager;
    private readonly IJobFolders _jobFolders;
    private readonly IJob _job;

    public JobVisualizerViewModel(
      IWindowManager windowManager,
      IJobFolders jobFolders, 
      IJob job)
    {
      if (windowManager == null) throw new ArgumentNullException("windowManager");
      if (jobFolders == null) throw new ArgumentNullException("jobFolders");
      if (job == null) throw new ArgumentNullException("job");

      _windowManager = windowManager;
      _jobFolders = jobFolders;
      _job = job;
    }

    protected override void OnActivate()
    {
      Books = new ReadOnlyCollection<BookViewModel>(
        _job.Books.Select(b => new BookViewModel(b)).ToList());

      base.OnActivate();
    }

    public ReadOnlyCollection<BookViewModel> Books { get; private set; }

    public async Task SaveJobJsx()
    {
      try {
        using (var stream = File.CreateText(_jobFolders.BookBuilderOutputScriptPath))
        {
          var localScript = await ComposeBookBuilderScript(_jobFolders.BookBuilderBaseScriptPath);
          await stream.WriteAsync(localScript);
        }

        using (var stream = File.CreateText(_jobFolders.BarcodeBuilderOutputScriptPath))
        {
          var barcodeScript = await ComposeBookBuilderScript(_jobFolders.BarcodeBuilderBaseScriptPath);
          await stream.WriteAsync(barcodeScript);
        }

        using (var data = File.CreateText(_jobFolders.JobDataOutputScriptPath))
        {
          await data.WriteAsync(_job.ToJsxString(0));
        }

        var messageWindow = new MessageWindowViewModel("Script files saved successfully!")
          { DisplayName = "Success!" };

        _windowManager.ShowDialog(messageWindow);
      }

      catch (Exception exc) {
        var errorWindow = new ErrorWindowViewModel();
        var errorString = exc.Message;
        errorWindow.DisplayName = "File export error";
        errorWindow.Errors = errorString;

        _windowManager.ShowDialog(errorWindow);
      }
    }

    private string BuildIncludePaths(bool isBookBuilder)
    {
      var sb = new StringBuilder();

      sb.AppendFormat("#include \"{0}\";", _jobFolders.Ean13IncludeScriptPath);
      sb.AppendFormat("\n#include \"{0}\";", _jobFolders.JobDataOutputScriptPath);

      if (isBookBuilder) {
        sb.AppendFormat("\n#include \"{0}\";", _jobFolders.UiScriptIncludePath);
      }

      sb.AppendLine();

      return sb.ToString().FlipSlashes();
    }

    private async Task<string> ComposeBookBuilderScript(string baseScriptPath)
    {
      var isBookBuilder = baseScriptPath == _jobFolders.BookBuilderBaseScriptPath;

      if (!File.Exists(baseScriptPath)) {
        throw new FileNotFoundException(
          string.Format("Unable to locate the base InDesign script fragment '{0}'", 
            Path.GetFileName(baseScriptPath)));
      }

      string baseScript;
      using (var baseScriptFile = File.OpenText(baseScriptPath)) {
        baseScript = await baseScriptFile.ReadToEndAsync();
      }

      var sb = new StringBuilder(BuildIncludePaths(isBookBuilder));
      sb.AppendLine(baseScript);

      return sb.ToString();
    }
  }
}