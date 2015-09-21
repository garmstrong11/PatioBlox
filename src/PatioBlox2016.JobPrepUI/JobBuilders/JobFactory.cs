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

      var books = _extractionResult.BookGroups
        .Select(bg => _bookFactory.CreateBook(job, bg.Key, bg));

      job.AddBookRange(books);

      return job;
    }
  }
}