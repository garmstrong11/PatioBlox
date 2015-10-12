namespace PatioBlox2016.Abstract
{
  public interface IReporter
  {
    string TemplatePath { get; set; }
    string OutputPath { get; set; }

    void BuildDtoList(string storeListPath);
    void Run();
  }
}