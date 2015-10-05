namespace PatioBlox2016.JobPrepUI.JobVisualizer
{
  using System;
  using System.Collections.ObjectModel;
  using System.IO;
  using System.Linq;
  using Caliburn.Micro;
  using Abstract;
  using Services.Contracts;

  public class JobVisualizerViewModel : Screen
  {
    private readonly IExtractionResultValidationUow _uow;
    private readonly IJobFolders _jobFolders;
    private readonly IJob _job;

    public JobVisualizerViewModel(
      IExtractionResultValidationUow uow,
      IJobFolders jobFolders, 
      IJob job)
    {
      if (uow == null) throw new ArgumentNullException("uow");
      if (jobFolders == null) throw new ArgumentNullException("jobFolders");
      if (job == null) throw new ArgumentNullException("job");

      _uow = uow;
      _jobFolders = jobFolders;
      _job = job;
    }

    protected override void OnActivate()
    {
      _job.PopulateJob(_uow.GetBookGroups());
      _job.AddDescriptionRange(_uow.GetDescriptionsForJob());

      Books = new ReadOnlyCollection<BookViewModel>(
        _job.Books.Select(b => new BookViewModel(b)).ToList());

      base.OnActivate();
    }

    public ReadOnlyCollection<BookViewModel> Books { get; private set; }

    public void SaveJobJsx()
    {
      var jsx = _job.ToJsxString(0);
      var dataFilePath = Path.Combine(_jobFolders.JsxDir.FullName, "JobData.jsx");

      File.WriteAllText(dataFilePath, jsx);
    }
  }
}