namespace PatioBlox2018.Impl.AbstractReporter
{
  public interface IReporter
  {
    void BuildReport();
    string OutputPath { get; }
  }
}