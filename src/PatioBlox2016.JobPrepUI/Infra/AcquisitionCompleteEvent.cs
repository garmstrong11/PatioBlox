namespace PatioBlox2016.JobPrepUI.Infra
{
  using FluentValidation.Results;

  public class AcquisitionCompleteEvent
  {
    public int RowCount { get; set; }
    public ValidationResult ValidationResult { get; set; }
  }
}