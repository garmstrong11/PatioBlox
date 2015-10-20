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
      var jobDataPath = _jobFolders.JobDataOutputScriptPath;
      var bookBuilderPath = _jobFolders.BookBuilderOutputScriptPath;

      try {
        using (var stream = File.CreateText(bookBuilderPath)) {
          var localScript = await BuildLocalScript();
          await stream.WriteAsync(localScript);
        }

        using (var data = File.CreateText(jobDataPath)) {
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

    private string BuildIncludePaths()
    {
      var sb = new StringBuilder();

      sb.AppendFormat("#include \"{0}\";", _jobFolders.UiScriptIncludePath);
      sb.AppendFormat("\n#include \"{0}\";", _jobFolders.Ean13IncludeScriptPath);
      sb.AppendFormat("\n#include \"{0}\";\n", Job.JobDataFileName);

      return sb.ToString().FlipSlashes();
    }

    private async Task<string> BuildLocalScript()
    {
      var baseScriptPath = Path.Combine(_jobFolders.BookBuilderBaseScriptPath);

      if (!File.Exists(baseScriptPath)) {
        throw new FileNotFoundException(
          string.Format("Unable to locate the base InDesign script fragment '{0}'", 
            Path.GetFileName(baseScriptPath)));
      }

      string baseScript;
      using (var baseScriptFile = File.OpenText(baseScriptPath)) {
        baseScript = await baseScriptFile.ReadToEndAsync();
      }

      var sb = new StringBuilder(BuildIncludePaths());
      sb.AppendLine(baseScript);

      return sb.ToString();
    }
  }
}