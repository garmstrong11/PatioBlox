namespace PatioBlox2016.JobPrepUI.JobBuilders
{
  using PatioBlox2016.Concrete;

  public interface IDescriptionFactory
  {
    Description CreateDescription(string descriptionText);
    void UpdateKeywordDict();
  }
}