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
  using Services.Contracts;

  public class JobVisualizerViewModel : Screen
  {
    private readonly IWindowManager _windowManager;
    private readonly IExtractionResultValidationUow _uow;
    private readonly IJobFolders _jobFolders;
    private readonly IJob _job;
    private static readonly string BookBuilderScriptName = "BookBuilder.jsx";

    public JobVisualizerViewModel(
      IWindowManager windowManager,
      IExtractionResultValidationUow uow,
      IJobFolders jobFolders, 
      IJob job)
    {
      if (windowManager == null) throw new ArgumentNullException("windowManager");
      if (uow == null) throw new ArgumentNullException("uow");
      if (jobFolders == null) throw new ArgumentNullException("jobFolders");
      if (job == null) throw new ArgumentNullException("job");

      _windowManager = windowManager;
      _uow = uow;
      _jobFolders = jobFolders;
      _job = job;
    }

    protected override void OnActivate()
    {
      //_job.PopulateJob(_uow.GetBookGroups());
      //_job.AddDescriptionRange(_uow.GetDescriptionsForJob());

      Books = new ReadOnlyCollection<BookViewModel>(
        _job.Books.Select(b => new BookViewModel(b)).ToList());

      base.OnActivate();
    }

    public ReadOnlyCollection<BookViewModel> Books { get; private set; }

    public async Task SaveJobJsx()
    {
      var dataFilePath = Path.Combine(_jobFolders.JsxDir.FullName, Job.JobDataFileName);
      var resultPath = Path.Combine(_jobFolders.JsxDir.FullName, BookBuilderScriptName);

      try {
        using (var stream = File.CreateText(resultPath)) {
          var localScript = await BuildLocalScript();
          await stream.WriteAsync(localScript);
        }

        using (var data = File.CreateText(dataFilePath)) {
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
      var uiScriptPath = Path.Combine(_jobFolders.FactoryScriptsDir.FullName, "ui.jsx");
      var eanScriptPath = Path.Combine(_jobFolders.FactoryScriptsDir.FullName, "EAN-13.jsx");

      sb.AppendFormat("#include \"{0}\";", uiScriptPath);
      sb.AppendFormat("\n#include \"{0}\";", eanScriptPath);
      sb.AppendFormat("\n#include \"{0}\";\n", Job.JobDataFileName);

      return sb.ToString().FlipSlashes();
    }

    private async Task<string> BuildLocalScript()
    {
      var baseScriptPath = Path.Combine(_jobFolders.FactoryScriptsDir.FullName, BookBuilderScriptName);

      if (!File.Exists(baseScriptPath)) {
        throw new FileNotFoundException(
          string.Format("Unable to locate the base InDesign script fragment '{0}'", BookBuilderScriptName));
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