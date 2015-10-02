namespace PatioBlox2016.JobPrepUI.JobVisualizer
{
  using System;
  using System.IO;
  using Caliburn.Micro;
  using PatioBlox2016.Abstract;
  using PatioBlox2016.Services.Contracts;

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
      base.OnActivate();
    }

    public void SaveJobJsx()
    {
      var jsx = _job.ToJsxString(0);
      var dataFilePath = Path.Combine(_jobFolders.JsxDir.FullName, "JobData.jsx");

      File.WriteAllText(dataFilePath, jsx);
    }
  }
}