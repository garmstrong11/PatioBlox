namespace PatioBlox2016.Abstract
{
  public interface ICellFactory
  {
    ICell CreateCell(IPatchRowExtract extract);
  }
}