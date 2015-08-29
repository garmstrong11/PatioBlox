namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using Caliburn.Micro;

  public interface IJobFileDropViewModel : IScreen
  {
    void HandleFileDrag(object evtArgs);
    int ExtractionProgress { get; set; }
    string Status { get; set; }
  }
}