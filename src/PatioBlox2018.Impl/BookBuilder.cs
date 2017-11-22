namespace PatioBlox2018.Impl
{
  using PatioBlox2018.Core;

  public class BookBuilder
  {
    private IExtractionResult ExtractionResult { get; }

    public BookBuilder(IExtractionResult extractionResult)
    {
      ExtractionResult = extractionResult;
    }

    public ScanbookBook BuildBook(string patchName) =>
      new ScanbookBook(ExtractionResult.GetRowsForPatch(patchName));
  }
}