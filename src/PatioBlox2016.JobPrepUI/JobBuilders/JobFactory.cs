namespace PatioBlox2016.JobPrepUI.JobBuilders
{
  using System.Linq;
  using Concrete;
  using Extractor;

  public class JobFactory : IJobFactory
  {
    private readonly IBookFactory _bookFactory;
    private readonly IExtractionResult _extractionResult;

    public JobFactory(IBookFactory bookFactory, IExtractionResult extractionResult)
    {
      _bookFactory = bookFactory;
      _extractionResult = extractionResult;
    }
    
    public Job CreateJob()
    {
      var job = new Job();
      foreach (var patchName in _extractionResult.PatchNames) {
        var name = patchName;
        var extracts = _extractionResult.PatchRowExtracts.Where(p => p.PatchName == name);
        job.AddBook(_bookFactory.CreateBook(job, name, extracts));
      }

      return job;
    }
  }
}