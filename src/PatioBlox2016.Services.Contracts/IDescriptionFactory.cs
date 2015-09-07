namespace PatioBlox2016.Services.Contracts
{
  using Concrete;

  public interface IDescriptionFactory
  {
    Description CreateDescription(string descriptionText);
  }
}