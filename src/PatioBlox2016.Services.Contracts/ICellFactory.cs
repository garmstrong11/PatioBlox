namespace PatioBlox2016.Services.Contracts
{
  using Abstract;
  using Concrete;

  public interface ICellFactory
  {
    Cell CreateCell(IPatchRowExtract extract);
  }
}