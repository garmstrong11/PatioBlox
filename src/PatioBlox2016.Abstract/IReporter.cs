namespace PatioBlox2016.Abstract
{
  using System.Threading.Tasks;

  public interface IReporter
  {
    void Initialize(string storeListPath);
    Task BuildPatchReport(string templatePath, string outputPath);
    Task BuildMetrixCsv(string outputPath);

    bool IsInitialized { get; }
  }
}